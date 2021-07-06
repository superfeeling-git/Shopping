using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.SqlClient;
using System.IO;

namespace Shopping.UI.Controllers
{
    public class GoodsListController : Controller
    {
        // GET: GoodsList
        public ActionResult Index()
        {
            string str = "12345";
            string admin = $"admin{str}";
            FormsAuthentication.HashPasswordForStoringInConfigFile(password: admin, passwordFormat:"");


            return View();
        }
    }
}