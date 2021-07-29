using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Model
{
    public class PriceModel
    {
        public int RangeID { get; set; }
        public string RangeName { get; set; }
        public Nullable<int> RangeOrder { get; set; }
    }
}
