using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopping.Model;
using Shopping.Bll;

namespace Shopping.UI.Controllers
{
    public class SharedController : Controller
    {
        DictBLL dictBLL = new DictBLL();
        GoodsCategoryBLL goodsCategoryBLL = new GoodsCategoryBLL();

        [ChildActionOnly]
        public ActionResult Top()
        {
            ViewBag.dict = dictBLL.GetListForType(1);
            ViewBag.category = goodsCategoryBLL.GetAll();
            return View();
        }
    }
}