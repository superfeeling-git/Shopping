using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopping.Bll;
using Shopping.Model;
using Newtonsoft.Json;
using System.Net.Mail;
using Webdiyer;
using Webdiyer.WebControls.Mvc;
using Shopping.UI.Filters;

namespace Shopping.UI.Controllers
{
    public class UserController : Controller
    {
        UserBLL userBLL = new UserBLL();
        
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            var s = "";
            string str = $"{s}";
            if(ModelState.IsValid)
                return Json(new { result = userBLL.Create(registerModel), msg = "注册成功" }, JsonRequestBehavior.AllowGet);
            return Json(new { result = 0, msg = "数据验证失败" }, JsonRequestBehavior.AllowGet);
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
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(RegisterModel loginModel)
        {
            return Json(userBLL.Login(loginModel),JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Logout()
        {
            Session["user"] = null;
            return Redirect("/");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserLogin()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ResetPassword(string email)
        {
            return Json(userBLL.ResetPassword(email), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [URLFilter]
        public ActionResult ChangePassword(string email)
        {
            ViewBag.email = email;
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [URLFilter]
        public ActionResult ChangePassword(UserModel userModel)
        {
            userBLL.ResetPassword(userModel);
            return RedirectToAction("Login");
        }
    }
}