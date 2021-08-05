using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Model;

namespace Shopping.Dal
{
    public class OrderDAL
    {
        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="shopping"></param>
        /// <returns></returns>
        public int CreateOrder(ShoppingOrderModel shopping)
        {
            ShoppingEntities db = new ShoppingEntities();
            db.ShoppingOrder.Add(
                new ShoppingOrder             
                {
                    Address = shopping.Address,
                    Area = shopping.Area,
                    City = shopping.City,
                    FullName = shopping.FullName,
                    OrderNum = shopping.OrderNum,
                    OrderStatus = shopping.OrderStatus,
                    OrderTime = shopping.OrderTime,
                    Phone = shopping.Phone,
                    Province = shopping.Province,
                    UserID = shopping.UserID,
                    OrderGoods = shopping.OrderGoodsModel.MapToList<OrderGoodsModel,OrderGoods>()
                }
             );

            return db.SaveChanges();
        }
    }
}
