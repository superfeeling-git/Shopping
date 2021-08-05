using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopping.Bll;

namespace Shopping.UI.Filters
{
    /// <summary>
    /// 判断用户是否登录
    /// </summary>
    public class AuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!UserContext.IsLogin)
            {
                filterContext.Result = new RedirectResult("/User/Login");
            }
        }
    }
}