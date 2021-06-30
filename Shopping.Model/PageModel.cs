using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Model
{
    public class PageModel<T>
    {
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
        public List<T> PageData { get; set; }
    }

    public class PageSetting
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
    }
}
