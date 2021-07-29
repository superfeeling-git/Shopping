using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Model
{
    public class ResetPasswordModel
    {
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        [Compare("Password",ErrorMessage = "两次密码不一致")]
        public string ConfirmPassword { get; set; }
    }
}
