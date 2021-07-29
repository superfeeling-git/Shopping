using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping.UI.Controllers
{
    public class CookiesTestController : Controller
    {
        // GET: CookiesTest
        public ActionResult Index()
        {
            HttpCookie cookie = new HttpCookie("name");
            cookie.Expires = DateTime.Now.AddMinutes(10);
            cookie.Value = "abcd";
            cookie.HttpOnly = true;

            Response.Cookies.Add(cookie);

            return null;
        }

        // GET: CookiesTest
        public ActionResult getCookies()
        {
            var httpCookies = Request.Cookies["name"];
            if(httpCookies != null)
                Response.Write(httpCookies.Value);

            return null;
        }

        /// <summary>
        /// 清空Cookies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ClearCookies()
        {
            //var httpCookies = Request.Cookies["name"];

            //Response.Cookies.Remove("name");

            var cookies = Request.Cookies["name"];

            cookies.Expires = DateTime.Now.AddMinutes(-10);

            Response.Cookies.Add(cookies);

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult JsCookies()
        {
            return View();
        }
    }
}