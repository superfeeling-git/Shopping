using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Model
{
    public class GoodsQueryModel: QueryBaseModel
    {
        public int? CategoryID { get; set; }
        public int? RangeID { get; set; }
        public int? OutID { get; set; }
        public int? PlaceID { get; set; }
    }
}
