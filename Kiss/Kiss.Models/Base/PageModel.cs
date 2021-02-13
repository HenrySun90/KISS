using System;
using System.Collections.Generic;
using System.Text;

namespace Kiss.Models.Base
{
    public class PageModel<T>
    {
        public int Total { get; set; }
        public List<T> DataList { get; set; }
    }
}
