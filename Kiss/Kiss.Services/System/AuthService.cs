using Kiss.Common;
using Kiss.Models.System;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiss.Services.System
{
    /// <summary>
    /// 认证与授权服务类
    /// </summary>
    public class AuthService
    {
        private readonly KissContext _context;
        public AuthService(KissContext context)
        {
            _context = context;
        }

        public async Task<bool> TokenValid(string token)
        {
            return await _context.UserTokens.Where(
                    x => x.Token.ToString() == token && x.ExpireTime >DateTime.Now
                )
                .AnyAsync();
        }

        public async Task<User> GetUserByToken(string token)
        {
            var user =await _context.Users
                .Include(x => x.UserToken)
                .Where(x => x.UserToken.Token.ToString() == token)
                .Include(x => x.Role).FirstOrDefaultAsync();
            return user;
        }

        public async Task<string> Login(UserLoginDto dto, bool force=false)
        {
            // 1.验证账号密码
            // 2.验证是否已登陆且未过期
            // 3.颁发新的token
            if (! await UserValid(dto))
            {
                // Todo  抛出自定义错误-账号或密码错误
                throw new Exception("账号或密码错误");

            }
            var user = await _context.Users.Include(x => x.UserToken)
                .Where(x => x.UserToken.ExpireTime > DateTime.Now)
                .Where(x=>x.UserName==dto.UserName).FirstOrDefaultAsync();
            if (user != null && !force)
            {
                // Todo  抛出自定义错误-用户已登录
                throw new Exception("用户已登陆");
            }
            if (user!=null && force)
            {
                // 如果强制登陆，删除之前的token，然后创建新的
                var uToken = user.UserToken;
                _context.UserTokens.Remove(uToken);
                await _context.SaveChangesAsync();
            }

            var u = await _context.Users.Where(x => x.UserName == dto.UserName).FirstAsync();
            return await CreateToken(u);


        }

        /// <summary>
        /// 验证用户账号、密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> UserValid(UserLoginDto dto)
        {
            var hashPassword = Utils.MD5Hash(dto.Password);
            var user = await _context.Users
                .Where(x => x.UserName == dto.UserName && x.Password == hashPassword)
                .AnyAsync();
            
            return user;

        }

        /// <summary>
        /// 登陆成功、颁发token
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<string> CreateToken(User user)
        {

            UserToken uToken = new UserToken
            {
                CreateTime = DateTime.Now,
                UserId = user.Id,
                Token = Guid.NewGuid(),
                IssueTime = DateTime.Now,
                ExpireTime = DateTime.Now.AddMinutes(30)
            };
            _context.UserTokens.Add(uToken);
            await _context.SaveChangesAsync();
            return uToken.Token.ToString();
        }

        /// <summary>
        /// 延迟token过期时间
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public void DelayExpireTime(string token)
        {
            var uToken =  _context.UserTokens.Where(x => x.Token.ToString() == token).FirstOrDefault();
            uToken.ExpireTime = DateTime.Now.AddMinutes(30);
            _context.SaveChangesAsync();
        }
    }
}
