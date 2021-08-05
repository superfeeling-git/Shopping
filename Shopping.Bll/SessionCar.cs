using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Model;
using System.Web;
using Shopping.Dal;
using NLog;
using Newtonsoft.Json;

namespace Shopping.Bll
{
    public class SessionCar : ICar
    {
        private Logger log = LogManager.GetCurrentClassLogger();

        private GoodsDAL goodsDAL = new GoodsDAL();

        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="carModel"></param>
        /// <returns></returns>
        public List<CarModel> AddCar(CarModel carModel)
        {
            //获取SESSION的商品列表
            var list = GetCar();

            //判断有没有该商品，如果有数量；如果没有，该商品加入购物车
            var goods = list.FirstOrDefault(m => m.GoodsID == carModel.GoodsID);
            if (goods != null)
            {
                goods.BuyCount += carModel.BuyCount;
            }
            else
            {
                list.Add(carModel);
            }

            string str_list = JsonConvert.SerializeObject(list);

            //存回SESSION
            HttpCookie cookie = HttpContext.Current.Request.Cookies["car"];

            cookie.Value = str_list;

            HttpContext.Current.Response.Cookies.Add(cookie);

            return list;
        }

        /// <summary>
        /// 获取购物车所有商品
        /// </summary>
        /// <returns></returns>
        public List<CarModel> GetCar()
        {
            //购物车商品数据
            HttpCookie cookie = HttpContext.Current.Request.Cookies["car"];

            //空的处理
            if (cookie == null)
            {
                List<CarModel> list = new List<CarModel>();
                string str = JsonConvert.SerializeObject(list);

                //新建一个Cookies对象
                cookie = new HttpCookie("car");
                cookie.Value = str;//[]
                HttpContext.Current.Response.Cookies.Add(cookie);

                return list;
            }

            List<CarModel> carlist = JsonConvert.DeserializeObject<List<CarModel>>(cookie.Value);

            //联查数据库
            foreach (var item in carlist)
            {
                var dbGoodsModel = goodsDAL.GetModel(item.GoodsID);
                item.GoodsName = dbGoodsModel.GoodsName;
                item.Price = (int)dbGoodsModel.Price;
                item.GoodsPic = dbGoodsModel.GoodsPic.Split(',')[0];
            }

            return carlist;
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

            string str_list = JsonConvert.SerializeObject(list);

            //存回SESSION
            HttpCookie cookie = HttpContext.Current.Request.Cookies["car"];

            //给Cookies重新赋值 
            cookie.Value = str_list;

            //重新保存
            HttpContext.Current.Response.Cookies.Add(cookie);

            //重新保存商品信息到SESSION
            //HttpContext.Current.Session["car"] = list;

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
                /*
                var list = GetCar();

                //移除全部元素
                list.RemoveRange(0, list.Count - 1);

                //重新保存商品信息到SESSION
                HttpContext.Current.Session["car"] = list;
                */

                HttpCookie cookie = HttpContext.Current.Request.Cookies["car"];

                cookie.Expires = DateTime.Now.AddDays(-1);

                HttpContext.Current.Response.Cookies.Add(cookie);

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

            //存回SESSION
            HttpCookie cookie = HttpContext.Current.Request.Cookies["car"];

            string str_list = JsonConvert.SerializeObject(list);

            cookie.Value = str_list;

            HttpContext.Current.Response.Cookies.Add(cookie);

            //重新保存商品信息到SESSION
            //HttpContext.Current.Session["car"] = list;

            return list;
        }
    }
}
