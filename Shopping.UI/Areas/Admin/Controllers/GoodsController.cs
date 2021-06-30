using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Configuration;

namespace Shopping.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class GoodsController : Controller
    {
        // GET: Admin/Goods
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        public int MyProperty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            string Upload = "/UploadFile";

            //slajfalsdjf


            string ext = Path.GetExtension(file.FileName).Trim('.');

            string image = ConfigurationManager.AppSettings["image"];
            string video = ConfigurationManager.AppSettings["video"];
            string audio = ConfigurationManager.AppSettings["audio"];
            string attach = ConfigurationManager.AppSettings["attach"];

            var FolderName = string.Empty;

            if (image.Contains(ext))
            {
                FolderName = "image";
            }
            else if (video.Contains(ext))
            {
                FolderName = "video";
            }
            else if (audio.Contains(ext))
            {
                FolderName = "audio";
            }
            else if (attach.Contains(ext))
            {
                FolderName = "attach";
            }
            else
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }

            Upload = $"{Upload}/{FolderName}/{DateTime.Now.ToString("yyyyMM")}";

            if(!Directory.Exists(Server.MapPath(Upload)))
            {
                Directory.CreateDirectory(Server.MapPath(Upload));
            }



            //生成文件名
            var FileName = $"{Upload}/{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.{ext}";

            //保存文件
            file.SaveAs(Server.MapPath(FileName));
            
            return Json(new 
            {
                result = "ok",
                id = DateTime.Now.Ticks,
                url= FileName
            }, JsonRequestBehavior.AllowGet);
        }
    }
}