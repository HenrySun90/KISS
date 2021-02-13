using Kiss.Services.System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kiss.WebApi.Auth
{
    public class KissAuthHandler : IAuthenticationHandler
    {
        private readonly AuthService _service;
        private string FailMsg;
        public const string SchemeName = "KissAuth";
        AuthenticationScheme _scheme;
        HttpContext _context;

        public KissAuthHandler(AuthService service)
        {
            _service = service;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="scheme"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            _scheme = scheme;
            _context = context;
            return Task.CompletedTask;
        }

        /// <summary>
        /// 用户认证逻辑
        /// </summary>
        /// <returns></returns>
        public Task<AuthenticateResult> AuthenticateAsync()
        {
            // 1.获取请求头携带的Token
            // 2.在数据库中验证Token是否存在、是否过期
            // 3.签发用户票据
            var Authorization = _context.Request.Headers[HttpRequestHeader.Authorization.ToString()];
            if (Authorization.Count==0)
            {
                FailMsg = "{\"msg\":\"请求未包含Token\"}";
                return Task.FromResult(AuthenticateResult.Fail("未能获取到Token"));
            }
            string token = Authorization.ToString().Replace("Bearer ","");
           
            // 验证token是否正确，是否过期
            if (!_service.TokenValid(token).Result)
            {
                FailMsg = "{\"msg\":\"Token无效或者已经过期\"}";
                return Task.FromResult(AuthenticateResult.Fail(FailMsg));
            }

            var user = _service.GetUserByToken(token).Result;
            // 构造用户票据
            var claimsIdentity = new ClaimsIdentity(new Claim[] 
            { 
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.Name)
            }, "KissAuth");
            var principal = new ClaimsPrincipal(claimsIdentity);
            var ticket = new AuthenticationTicket(principal, _scheme.Name);

            // 活跃用户自动续期，当用户半小时不发送请求，则token失效
            _service.DelayExpireTime(token);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        /// <summary>
        /// 未登录处理
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            _context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            _context.Response.Headers.Add("Content-Type", "application/json; charset=UTF-8");
            _context.Response.WriteAsync(FailMsg);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 权限不足处理
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public Task ForbidAsync(AuthenticationProperties properties)
        {
            _context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return Task.CompletedTask;
        }

        
    }
}
