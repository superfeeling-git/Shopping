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
            string mono = Request.QueryString["mono"];
            if (!string.IsNullOrWhiteSpace(mono))
            {

            }
            userBLL.Create(userModel);
            return RedirectToAction("Create");
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
            var list = userBLL.GetPageDataTuple(pageSetting.PageSize, pageSetting.PageIndex, Keywords);

            ViewBag.keywords = Keywords;
            ViewBag.page = pageSetting;

            return View(list);
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

            var obj = new { Column= 1, Value= 2 };
            string json = JsonConvert.SerializeObject(obj);

            int id = 1;

            string str = "[{\"Column\":\""+ id + "\",\"Value\":\"235\"}]";

            return Json(new { info = "ok" }, JsonRequestBehavior.AllowGet);
        }
    }
}