using System;
using System.Collections.Generic;
using System.Text;

namespace Kiss.Models.Base
{
    public class BaseResult
    {
        public ResultCode Code { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }
    }

    public enum ResultCode 
    {
        success,
        error,
    }
}
