using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Model
{
    public class RegisterModel : UserModel
    {
        [Required(ErrorMessage = "请输入确认密码")]
        [Compare("Password",ErrorMessage = "两次密码不一致")]
        public string ConfirmPassword { get; set; }
    }
}
