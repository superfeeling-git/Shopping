using Shopping.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopping.Bll;
using PagedList;
using PagedList.Mvc;

namespace Shopping.UI.Controllers
{
    public class GoodsController : Controller
    {
        GoodsBLL goodsBLL = new GoodsBLL();
        PlaceBLL placeBLL = new PlaceBLL();
        PriceBLL priceBLL = new PriceBLL();
        OutMaterialBLL outMaterialBLL = new OutMaterialBLL();
        SessionCar car = new SessionCar();

        [HttpGet]
        public ActionResult Index(GoodsQueryModel goodsQuery, string Field = "GoodsID", string OrderBy = "DESC", int pageSize = 10, int PageIndex = 1)
        {
            ViewBag.place = placeBLL.GetAll();
            ViewBag.price = priceBLL.GetAll();
            ViewBag.@out = outMaterialBLL.GetAll(); 

            var list = goodsBLL.GetPageDataTuple(goodsQuery, Field, OrderBy, pageSize, PageIndex);

            return View(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// /Goods/Detail/20
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail(int id)
        {
            return View(goodsBLL.GetModel(id));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GoodsList(int PageIndex = 1,int PageSize = 8)
        {
            var Result = goodsBLL.GetPageDataTuple(PageSize, PageIndex);

            var list = new StaticPagedList<GoodsModel>(Result.Item3, PageIndex, PageSize, Result.Item1);

            return View(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddCar(CarModel carModel)
        {
            var list = car.AddCar(carModel);

            return Json(
                new
                {
                    GoodsCount = list.Count(),
                    TotalPrice = list.Sum(m => m.Price * m.BuyCount)
                }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddCarSuccess()
        {
            return View();
        }
    }
}