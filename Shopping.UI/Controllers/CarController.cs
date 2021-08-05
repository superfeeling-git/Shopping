using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopping.Bll;
using Shopping.Model;
using StackExchange.Redis;

namespace Shopping.UI.Controllers
{
    public class CarController : Controller
    {
        CarBLL Car = new CarBLL();

        public ActionResult Index()
        {
            return View(Car.GetCar());    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List1()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List()
        {
            var list1 = new List<GoodsModel>();
            GoodsModel goodsModel = new GoodsModel();
            goodsModel.CategoryID = 1;
            goodsModel.CategoryName = "贏料";
            goodsModel.GoodsName = "可樂";
            list1.Add(goodsModel);
            //ViewBag.list = list1;

            var list = new List<GoodsCategoryModel> 
            {
                 new GoodsCategoryModel
                 {
                      CategoryID = 1, CategoryName = "家电", GoodsModel = new List<GoodsModel>
                      {
                       new GoodsModel{ GoodsID = 1, GoodsName = "电视机", GoodsPic = "/images/img/02640.jpg" },
                       new GoodsModel{ GoodsID = 2, GoodsName = "冰箱", GoodsPic = "/images/img/02657.jpg" },
                       new GoodsModel{ GoodsID = 3, GoodsName = "洗衣机", GoodsPic = "/images/img/02674.jpg" },
                      }
                 },
                 new GoodsCategoryModel
                 {
                      CategoryID = 2, CategoryName = "家居", GoodsModel = new List<GoodsModel>
                      {
                       new GoodsModel{ GoodsID = 4, GoodsName = "床垫", GoodsPic = "/images/image/01.jpg" },
                       new GoodsModel{ GoodsID = 5, GoodsName = "电视柜", GoodsPic = "/images/image/02.jpg" },
                       new GoodsModel{ GoodsID = 6, GoodsName = "沙发", GoodsPic = "/images/image/03.jpg" },
                      }
                 }
            };

            return View(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(CarModel carModel)
        {
            ConnectionMultiplexer db = ConnectionMultiplexer.Connect("");

            var _db = db.GetDatabase(0);

            _db.ListLength(new RedisKey(""));

            return Json(Car.UpdateCar(carModel), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Del(int id)
        {
            return Json(Car.DelCar(id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BulkDel(int[] idList)
        {
            return Json(Car.BulkDelCar(idList), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Collapse()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Media()
        {
            return View();
        }
    }
}