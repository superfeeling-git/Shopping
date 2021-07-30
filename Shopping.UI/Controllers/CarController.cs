using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopping.Bll;
using Shopping.Model;

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
        [HttpPost]
        public ActionResult Update(CarModel carModel)
        {
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
    }
}