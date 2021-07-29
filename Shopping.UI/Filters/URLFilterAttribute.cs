using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Shopping.Common;

namespace Shopping.UI.Filters
{
    /// <summary>
    /// 自定义URL过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class URLFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 验证URL合法性
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //要验证的Email
            var email = filterContext.HttpContext.Request.QueryString["email"];

            //签名数据
            var sign = filterContext.HttpContext.Request.QueryString["sign"];

            //参数为空跳转到登录页面
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(sign))
            {
                filterContext.Result = new RedirectResult("/User/Login");
            }

            var key = ConfigurationManager.AppSettings["MailKey"];

            //得到加密结果
            var signer = $"{email}{key}".GetMD5();

            if(signer != sign)
            {
                filterContext.Result = new RedirectResult("/User/Login");
            }
        }
    }
}