using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Dal;
using Shopping.Model;
using Snowflake.Core;

namespace Shopping.Bll
{
    public class OrderBLL
    {
        OrderDAL orderDAL = new OrderDAL();
        UserDAL userDAL = new UserDAL();
        CarDAL carDAL = new CarDAL();
        CarBLL carBLL = new CarBLL();

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="shopping"></param>
        /// <returns></returns>
        public int CreateOrder(int addressid, string idList)
        {
            //根据地址ID获取地址信息
            var address = userDAL.GetAddressByAddressID(addressid);

            var worker = new IdWorker(1, 1);

            string[] arr = idList.Split(',');

            int[] idArr = Array.ConvertAll(arr, m => Convert.ToInt32(m));

            ShoppingOrderModel shopping = new ShoppingOrderModel {
                FullName = address.FullName,
                Address = address.Address,
                AddressID = address.AddressID,
                Area = address.Area,
                City = address.City,
                Phone = address.Phone,
                Province = address.Province,
                OrderNum = worker.NextId().ToString(),
                OrderStatus = 1,
                OrderTime = DateTime.Now,
                UserID = UserContext.GetUser.UserID,
                OrderGoodsModel = carDAL.GetOrderGoods(idArr, UserContext.GetUser.UserID)
            };

            int count = orderDAL.CreateOrder(shopping);

            //清空购物车
            carBLL.ClearCar();

            return count;
        }
    }
}
