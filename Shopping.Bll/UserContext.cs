using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Security.Principal;
using Shopping.Model;
using System.Web.Security;

namespace Shopping.Bll
{
    /// <summary>
    /// 用户上下文
    /// </summary>
    public class UserContext
    {
        /// <summary>
        /// 判断用户是否登录
        /// </summary>
        public static bool IsLogin {
            get
            {
                UserModel user = HttpContext.Current.Session["user"] as UserModel;
                return user == null ? false : true;
            }
        }

        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        public static UserModel GetUser
        {
            get
            {
                UserModel user = HttpContext.Current.Session["user"] as UserModel;

                return user;
            }
        }
    }
}
