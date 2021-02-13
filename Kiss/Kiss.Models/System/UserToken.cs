using Kiss.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kiss.Models.System
{
    public class UserToken : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public Guid Token { get; set; }
        public DateTime IssueTime { get; set; } = DateTime.Now;
        public DateTime ExpireTime { get; set; }
    }
}
