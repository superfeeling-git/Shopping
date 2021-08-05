using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopping.Model;
using Shopping.Bll;
using Shopping.UI.Filters;

namespace Shopping.UI.Controllers
{

    public class OrderController : Controller
    {
        UserBLL userBLL = new UserBLL();
        CarBLL carBLL = new CarBLL();
        OrderBLL orderBLL = new OrderBLL();

        /// <summary>
        /// 订单页面
        /// </summary>
        /// <returns></returns>
        [Auth]
        public ActionResult Index()
        {
            ViewBag.address = userBLL.GetAddress(UserContext.GetUser.UserID);

            string[] idList = Request.QueryString["idlist"].Split(',');

            int[] intList = Array.ConvertAll(idList, m => Convert.ToInt32(m));

            return View(carBLL.GetCars(intList));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveAddress(UserAddressModel userAddressModel)
        {
            return Json(new { id = userBLL.SaveAddress(userAddressModel) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateOrder(int addressid,string idList)
        {
            return Json(orderBLL.CreateOrder(addressid, idList), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Success()
        {
            return View();
        }
    }
}