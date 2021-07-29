using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using Shopping.Model;
using Shopping.Bll;

namespace Shopping.UI.Controllers
{
    public class HomeController : Controller
    {
        GoodsCategoryBLL goodsCategoryBLL = new GoodsCategoryBLL();
        GoodsBLL goodsBLL = new GoodsBLL();

        public ActionResult Index()
        {          
            ViewBag.category = goodsCategoryBLL.GetAll();

            ViewBag.IsToday = goodsBLL.GetGoods(4, m => m.IsToday);
            ViewBag.IsHot = goodsBLL.GetGoods(4, m => m.IsHot);
            ViewBag.IsNew = goodsBLL.GetGoods(4, m => m.IsNew);

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Slider()
        {
            return View();
        }
    }
}