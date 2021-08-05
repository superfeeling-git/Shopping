using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Model
{
    public class OrderGoodsModel
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int GoodsID { get; set; }
        public decimal Price { get; set; }
        public int BuyCount { get; set; }
    }
}
