using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Model;
using EntityFramework.Extensions;

namespace Shopping.Dal
{
    public class CarDAL
    {
        /// <summary>
        /// 添加商品到购物车
        /// </summary>
        /// <param name="carModel"></param>
        /// <returns></returns>
        public List<CarModel> AddCar(CarModel carModel)
        {
            ShoppingEntities db = new ShoppingEntities();
            db.ShoppingCar.Add(new ShoppingCar
            {
                BuyCount = carModel.BuyCount,
                GoodsID = carModel.GoodsID,
                UserID = carModel.UserID
            });
            db.SaveChanges();

            return GetCar(carModel.UserID);
        }

        /// <summary>
        /// 批量添加购物车
        /// </summary>
        /// <param name="carModel"></param>
        /// <returns></returns>
        public bool BulkAddCar(List<CarModel> carModel)
        {
            try
            {
                ShoppingEntities db = new ShoppingEntities();

                foreach (var item in carModel)
                {
                    db.ShoppingCar.Add(new ShoppingCar { GoodsID = item.GoodsID, BuyCount = item.BuyCount, UserID = item.UserID });
                }

                db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取购物车商品
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<CarModel> GetCar(int UserID)
        {
            ShoppingEntities db = new ShoppingEntities();
            return db.Goods.Join(db.ShoppingCar, a => a.GoodsID, b => b.GoodsID, (a, b) => new CarModel
            {
                BuyCount = b.BuyCount,
                GoodsID = a.GoodsID,
                GoodsName = a.GoodsName,
                GoodsPic = a.GoodsPic,
                Price = (int)a.Price,
                UserID = b.UserID
            }).Where(m => m.UserID == UserID).ToList();
        }

        /// <summary>
        /// 更新购物车
        /// </summary>
        /// <param name="carModel"></param>
        /// <returns></returns>
        public List<CarModel> UpdateCar(CarModel carModel)
        {
            ShoppingEntities db = new ShoppingEntities();
            db.ShoppingCar
                .Where
                (
                    m => m.UserID == carModel.UserID && m.GoodsID == carModel.GoodsID
                )
                .Update(
                m => new ShoppingCar
                {
                    BuyCount = carModel.BuyCount
                });

            return GetCar(carModel.UserID);
        }

        /// <summary>
        /// 删除购物车
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CarModel> DelCar(CarModel carModel)
        {
            ShoppingEntities db = new ShoppingEntities();
            db.ShoppingCar
                 .Where
                 (
                     m => m.UserID == carModel.UserID && m.GoodsID == carModel.GoodsID
                 )
                 .Delete();

            return GetCar(carModel.UserID);
        }

        /// <summary>
        /// 批量删除购物车
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public List<CarModel> BulkDelCar(int[] idList, int UserID)
        {
            ShoppingEntities db = new ShoppingEntities();

            db.ShoppingCar.Where(m => idList.Contains(m.GoodsID) && m.UserID == UserID).Delete();

            return GetCar(UserID);
        }

        /// <summary>
        /// 根据购物车表和商品表，得到某个用户选择（要）购买的商品
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public List<OrderGoodsModel> GetOrderGoods(int[] idList, int UserID)
        {
            ShoppingEntities db = new ShoppingEntities();

            var list = db.Goods.Join(db.ShoppingCar.Where(m => m.UserID == UserID), 
                a => a.GoodsID, 
                b => b.GoodsID, 
                (a, b) => new OrderGoodsModel
                {
                    BuyCount = b.BuyCount,
                    GoodsID = a.GoodsID,
                    Price = a.Price
                }).AsQueryable().Where(m => idList.Contains(m.GoodsID)).ToList();

            return list;
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <returns></returns>
        public bool ClearCar(int UserID)
        {
            ShoppingEntities db = new ShoppingEntities();

            db.ShoppingCar.Where(m => m.UserID == UserID).Delete();

            return true;
        }
    }
}
