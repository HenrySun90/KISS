using Kiss.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kiss.Models.System
{
    /// <summary>
    /// 角色实体类
    /// </summary>
    public class Role : BaseEntity
    {
        public string Name { get; set; }

    }

    /// <summary>
    /// 角色创建DTO
    /// </summary>
    public class RoleCreateDto
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// 角色修改DTO
    /// </summary>
    public class RoleUpdateDto : RoleCreateDto
    {
    }
}
