using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Configuration;
using Shopping.Bll;
using Shopping.Model;
using System.Data.SqlClient;

namespace Shopping.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class GoodsController : Controller
    {
        GoodsCategoryBLL GoodsCategoryBLL = new GoodsCategoryBLL();
        GoodsBLL goodsBLL = new GoodsBLL();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List(PageSetting pageSetting, string Keywords)
        {
            var list = goodsBLL.GetPageDataTuple(pageSetting.PageSize, pageSetting.PageIndex, Keywords);

            ViewBag.keywords = Keywords;
            ViewBag.page = pageSetting;

            return View(list);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.selectLists = new SelectList(GoodsCategoryBLL.GetAll(), "CategoryID", "CategoryName");
            return View(new GoodsModel());
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Create(GoodsModel goodsModel)
        {
            return Json(goodsBLL.Create(goodsModel), JsonRequestBehavior.AllowGet);
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