using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Shopping.Model
{
    /// <summary>
    /// 说明 用户表
    /// </summary>
    [Serializable]
    public class UserModel
    {
        #region 公共属性
        ///<Summary>
        /// 用户ID
        ///</Summary>
        public int UserID { get; set; }
        ///<Summary>
        /// 用户名
        ///</Summary>
        [Required(ErrorMessage = "请输入用户名")]
        [StringLength(10,MinimumLength = 3,ErrorMessage = "长度范围：3-10")]
        [Remote("CheckName", "User",AdditionalFields = "UserID", ErrorMessage = "用户名重复")]
        public string UserName { get; set; }
        ///<Summary>
        /// 密码
        ///</Summary>
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }
        ///<Summary>
        /// 姓名
        ///</Summary>
        [Required(ErrorMessage = "请输入姓名")]
        public string FullName { get; set; }
        ///<Summary>
        /// 性别
        ///</Summary>
        public bool Sex { get; set; }
        ///<Summary>
        /// 生日
        ///</Summary>
        public DateTime? Birthday { get; set; }
        ///<Summary>
        /// 手机号
        ///</Summary>
        [RegularExpression(@"^((13[0-9])|(15[^4,\D])|(18[0-9])|(17[0,3-9]))\d{8}$", ErrorMessage = "请输入正确的手机号")]
        public string HandPhone { get; set; }
        ///<Summary>
        /// 是否锁定
        ///</Summary>
        public bool? IsLock { get; set; }
        ///<Summary>
        /// 末次登录时间
        ///</Summary>
        public DateTime? LastLoginTime { get; set; }
        ///<Summary>
        /// 末次登录IP
        ///</Summary>
        public string LastLoginIP { get; set; }
        ///<Summary>
        /// 注册时间
        ///</Summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        #endregion
    }
}
