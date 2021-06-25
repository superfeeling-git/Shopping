using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Model;
using Shopping.Dal;
using Shopping.Common;
using System.Web;
using System.Web.Security;

namespace Shopping.Bll
{
    public class UserBLL
    {
        UserDAL userDAL = new UserDAL();

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool IsExists(string UserName)
        {
            return userDAL.IsExists(UserName);
        }

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool IsExists(string UserName,int UserID)
        {
            return userDAL.IsExists(UserName, UserID);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public int Create(UserModel userModel)
        {
            userModel.Password = userModel.Password.GetMD5();
            userModel.IsLock = false;
            userModel.LastLoginIP = null;
            userModel.LastLoginTime = null;
            userModel.CreateTime = DateTime.Now;

            return userDAL.Create(userModel);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public Tuple<int, int, List<UserModel>> GetPageDataTuple(int pageSize, int PageIndex, string Keywords)
        {
            return userDAL.GetPageDataTuple(pageSize, PageIndex, Keywords);
        }

        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserModel GetUser(int id)
        {
            return userDAL.GetUser(id);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public int Update(UserModel userModel)
        {
            return userDAL.Update(userModel);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginModel"></param>
        public ResultModel Login(LoginModel loginModel)
        {
            if(loginModel.ValidateCode.ToLower() != HttpContext.Current.Session["code"].ToString().ToLower())
            {
                return new ResultModel { ErrorCode = 1, Info = "验证码无效" };
            }
            else
            {
                var user = userDAL.IsExistsForUser(loginModel.UserName);

                if(user == null)
                {
                    return new ResultModel { ErrorCode = 2, Info = "无此用户" };
                }
                else
                {
                    if(loginModel.Password.GetMD5() == user.Password)
                    {
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(20), true, "userData");

                        string ticketCode = FormsAuthentication.Encrypt(ticket);

                        HttpCookie httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketCode);

                        httpCookie.Expires = DateTime.Now.AddMinutes(20);

                        httpCookie.HttpOnly = true;

                        HttpContext.Current.Response.Cookies.Add(httpCookie);

                        return new ResultModel { ErrorCode = 0, Info = "登录成功" };
                    }
                    else
                    {
                        return new ResultModel { ErrorCode = 3, Info = "密码错误" };
                    }
                }
            }
        }
    }
}
