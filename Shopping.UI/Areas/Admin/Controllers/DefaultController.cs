using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using Shopping.Model;
using Shopping.Bll;

namespace Shopping.UI.Areas.Admin.Controllers
{
    public class DefaultController : Controller
    {
        UserBLL UserBLL = new UserBLL();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Main()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            return Json(UserBLL.Login(loginModel), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ValidateCode()
        {
            int codelength = 6;

            //1、生成验证码的字符串
            string code = GeneralCode(codelength);

            Session["code"] = code;

            //2、把字符串放到图片里
            //位图宽高
            Bitmap bitmap = new Bitmap(codelength * 15, 24);

            //画布大小
            Graphics graphics = Graphics.FromImage(bitmap);

            //用白色填充
            graphics.Clear(Color.White);

            //定义字体
            Font font = new Font("微软雅黑", 14, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);

            //定义矩形
            Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            //定义线性渐变
            LinearGradientBrush brush = new LinearGradientBrush(rectangle, Color.Red, Color.Blue, 30);

            //画字符
            graphics.DrawString(code, font, brush, rectangle);

            Random random = new Random(unchecked((int)DateTime.Now.Ticks));

            for (int i = 0; i < 20; i++)
            {
                graphics.DrawLine(new Pen(Color.FromArgb(180, 190, 170, 110)), random.Next(bitmap.Width), random.Next(bitmap.Height), random.Next(bitmap.Width), random.Next(bitmap.Height));
            }

            MemoryStream memoryStream = new MemoryStream();

            bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);


            return File(memoryStream.ToArray(), "png");
        }


        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        private string GeneralCode(int len)
        {
            //全部字符串
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                stringBuilder.Append(i);
            }

            for (int i = 65; i < 91; i++)
            {
                stringBuilder.Append((char)i);
            }

            for (int i = 97; i < 123; i++)
            {
                stringBuilder.Append((char)i);
            }

            Random random = new Random(unchecked((int)DateTime.Now.Ticks));

            StringBuilder code = new StringBuilder();

            string codeList = stringBuilder.ToString();

            for (int i = 0; i < len; i++)
            {
                code.Append(codeList[random.Next(0, codeList.Length - 1)]);
            }         


            return code.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult t1()
        {
            Session["abc"] = "123";
            return new EmptyResult();
        }
    }
}