using Kiss.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kiss.Models.System
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    public class User : BaseEntity
    {
        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        [Required, MaxLength(20)]
        public string UserName { get; set; }

        [Required, MaxLength(20)]
        public string DisplayName { get; set; }

        [Required]
        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public UserToken UserToken { get; set; }
    }

    /// <summary>
    /// 用户创建DTO
    /// </summary>
    public class UserCreateDto
    {
        [Required]
        public int RoleId { get; set; }

        [Required, MaxLength(20)]
        public string UserName { get; set; }

        [Required, MaxLength(20)]
        public string DisplayName { get; set; }

        [Required, MinLength(6),MaxLength(16)]
        public string Password { get; set; }

        [Required, MinLength(6), MaxLength(16)]
        public string Password2 { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }

    /// <summary>
    /// 用户修改 DTO
    /// </summary>
    public class UserUpdateDto
    {
        [Required]
        public int RoleId { get; set; }

        [Required, MaxLength(20)]
        public string UserName { get; set; }

        [Required, MaxLength(20)]
        public string DisplayName { get; set; }

        [Required, MinLength(6), MaxLength(16)]
        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }

    /// <summary>
    /// 用户重设密码DTO
    /// </summary>
    public class UserResetPasswordDto
    {
        [Required, MinLength(6), MaxLength(16)]
        public string OldPassword { get; set; }

        [Required, MinLength(6), MaxLength(16)]
        public string NewPassword { get; set; }

        [Required, MinLength(6), MaxLength(16)]
        public string NewPassword2 { get; set; }
    }

    /// <summary>
    /// 用户登陆DTO
    /// </summary>
    public class UserLoginDto
    {
        [Required, MaxLength(20)]
        public string UserName { get; set; }

        [Required, MinLength(6), MaxLength(16)]
        public string Password { get; set; }
    }
}
