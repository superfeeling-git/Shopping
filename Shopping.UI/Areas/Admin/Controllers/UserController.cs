using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopping.Model;
using Shopping.Bll;
using Newtonsoft.Json;

namespace Shopping.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        UserBLL userBLL = new UserBLL();

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(UserModel userModel)
        {
            /*string mono = Request.QueryString["mono"];
            if (!string.IsNullOrWhiteSpace(mono))
            {

            }*/
            userBLL.Create(userModel);
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 验证用户是否存在
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckName(string UserName, int UserID)
        {
            return Json(!userBLL.IsExists(UserName, UserID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(PageSetting pageSetting, string Keywords)
        {
            if (Request.IsAjaxRequest())
            {
                var list = userBLL.GetPageDataTuple(pageSetting.PageSize, pageSetting.PageIndex, Keywords);

                ViewBag.keywords = Keywords;
                ViewBag.page = pageSetting;
                return Json(list, JsonRequestBehavior.AllowGet);
            }

            return View();
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
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(userBLL.GetUser(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(UserModel userModel)
        {
            userBLL.Update(userModel);

            string a = "张三";

            //我的名字是：张三

            string b = string.Format("我的名字是:{0}", a);

            string c = $"我的名字是:{a}";



            return Json(new { info = "ok" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            userBLL.Delete(id);
            return Redirect("/Admin/User?r=ok");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BatchDelete(int[] UserID)
        {
            return Json(userBLL.Delete(UserID), JsonRequestBehavior.AllowGet);
        }
    }
}