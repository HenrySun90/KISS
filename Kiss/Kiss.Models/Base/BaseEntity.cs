using System;
using System.Collections.Generic;
using System.Text;

namespace Kiss.Models.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string   CreateBy { get; set; }

        public DateTime UpdateTime { get; set; }
        public string UpdateBy { get; set; }
    }
}
