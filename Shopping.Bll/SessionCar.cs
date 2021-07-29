using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Model;
using System.Web;
using NLog;

namespace Shopping.Bll
{
    public class SessionCar
    {
        private Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="carModel"></param>
        /// <returns></returns>
        public List<CarModel> AddCar(CarModel carModel)
        {
            //获取SESSION的商品列表
            var list = GetCar();

            //判断有没有该商品，如果有数量+1；如果没有，该商品加入购物车
            var goods = list.FirstOrDefault(m => m.GoodsID == carModel.GoodsID);
            if(goods != null)
            {
                goods.BuyCount += 1;
            }
            else
            {
                list.Add(carModel);
            }

            //存回SESSION
            HttpContext.Current.Session["car"] = list;

            return list;
        }

        /// <summary>
        /// 获取购物车所有商品
        /// </summary>
        /// <returns></returns>
        public List<CarModel> GetCar()
        {
            //购物车商品数据
            var list = HttpContext.Current.Session["car"] as List<CarModel>;

            if(list == null)
            {
                list = new List<CarModel>();
                HttpContext.Current.Session["car"] = list;

                DateTime dt = new DateTime(5, 12, 28, 29, 30, 31);
            }

            //联查数据库

            return list;
        }

        /// <summary>
        /// 删除购物车商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CarModel> DelCar(int id)
        {
            var list = GetCar();

            //查询商品信息
            var goods = list.FirstOrDefault(m => m.GoodsID == id);

            //删除商品
            list.Remove(goods);

            //重新保存商品信息到SESSION
            HttpContext.Current.Session["car"] = list;

            return list;
        }

        /// <summary>
        /// 批量删除购物车商品
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public List<CarModel> BulkDelCar(int[] idList)
        {
            var list = GetCar();

            //删除包含指定ID的商品
            list.RemoveAll(m => idList.Contains(m.GoodsID));

            //重新保存商品信息到SESSION
            HttpContext.Current.Session["car"] = list;

            return list;
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <returns></returns>
        public bool ClearCar()
        {
            try
            {
                var list = GetCar();

                //移除全部元素
                list.RemoveRange(0, list.Count - 1);

                //重新保存商品信息到SESSION
                HttpContext.Current.Session["car"] = list;

                return true;
            }
            catch (Exception e)
            {
                log.Error(e);

                return false;
            }
        }

        /// <summary>
        /// 更新购物车
        /// </summary>
        /// <param name="carModel"></param>
        /// <returns></returns>
        public List<CarModel> UpdateCar(CarModel carModel)
        {
            var list = GetCar();

            var goods = list.FirstOrDefault(m => m.GoodsID == carModel.GoodsID);

            //更新
            goods.BuyCount = carModel.BuyCount;

            //重新保存商品信息到SESSION
            HttpContext.Current.Session["car"] = list;

            return list;
        }
    }
}
