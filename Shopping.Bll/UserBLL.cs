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
using NLog;
using System.Configuration;

namespace Shopping.Bll
{
    public class UserBLL
    {
        UserDAL userDAL = new UserDAL();

        Logger logger = LogManager.GetCurrentClassLogger();

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

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginModel"></param>
        public ResultModel Login(RegisterModel registerModel)
        {
            var user = userDAL.IsExistsForUser(registerModel.UserName);

            if (user == null)
            {
                return new ResultModel { ErrorCode = 2, Info = "无此用户" };
            }
            else
            {
                if (registerModel.Password.GetMD5() == user.Password)
                {
                    HttpContext.Current.Session["user"] = user;

                    return new ResultModel { ErrorCode = 0, Info = "登录成功" };
                }
                else
                {
                    return new ResultModel { ErrorCode = 3, Info = "密码错误" };
                }
            }

        }


        public int Delete(int id)
        {
            return userDAL.Delete(id);
        }

        public int Delete(int[] idList)
        {
            return userDAL.Delete(idList);
        }

        /// <summary>
        /// 根据邮箱判断是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool IsExistsByEmail(string Email)
        {
            return userDAL.IsExistsByEmail(Email);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public ResultModel ResetPassword(string Email)
        {
            //可以发送邮件
            if (IsExistsByEmail(Email))
            {
                //构造邮件内容：URL
                var httpcontext = HttpContext.Current;
                string Url = $"{httpcontext.Request.Url.Scheme}://{httpcontext.Request.Url.Authority}/user/ChangePassword?email={Email}";
                var sign = $"{Email}{ConfigurationManager.AppSettings["MailKey"]}".GetMD5();
                Url += $"&sign={sign}";
                logger.Info($"生成的邮件URL：{Url}");

                //发送邮件
                EmailHelper.Send(new FromEmail
                {
                    From = ConfigurationManager.AppSettings["from"],
                    Password = ConfigurationManager.AppSettings["password"],
                    smtp = ConfigurationManager.AppSettings["smtp"]
                }, "重置密码", $"<a href='{Url}' target='_blank'>{Url}</a>",
                new List<string> { Email }
                );
            }
            else
            {
                logger.Info(Email);
                return new ResultModel { ErrorCode = 1, Info = "邮件地址不存在" };
            }

            return new ResultModel { ErrorCode = 0, Info = "" };
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public int ResetPassword(UserModel userModel)
        {
            userModel.Password = userModel.Password.GetMD5();
            return userDAL.ResetPassword(userModel);
        }
    }
}
