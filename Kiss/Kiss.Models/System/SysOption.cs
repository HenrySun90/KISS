using Kiss.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kiss.Models.System
{
    public class SysOption : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
