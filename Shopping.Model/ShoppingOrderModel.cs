using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Model
{
    public class ShoppingOrderModel
    {
        public int OrderID { get; set; }
        public int AddressID { get; set; }
        public int UserID { get; set; }
        public string OrderNum { get; set; }
        public System.DateTime OrderTime { get; set; }
        public string FullName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public byte OrderStatus { get; set; }
        public List<OrderGoodsModel> OrderGoodsModel { get; set; }
    }
}
