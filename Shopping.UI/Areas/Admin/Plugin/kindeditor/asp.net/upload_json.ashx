<%@ webhandler Language="C#" class="Upload" %>

/**
 * KindEditor ASP.NET
 *
 * 本ASP.NET程序是演示程序，建议不要直接在实际项目中使用。
 * 如果您确定直接使用本程序，使用之前请仔细确认相关安全设置。
 *
 */

using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using System.Configuration;
using Newtonsoft.Json;

public class Upload : IHttpHandler
{
    private HttpContext context;

    public void ProcessRequest(HttpContext context)
    {
        /*String aspxUrl = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf("/") + 1);

        //文件保存目录路径
        String savePath = "../attached/";

        //文件保存目录URL
        String saveUrl = aspxUrl + "../attached/";*/

        //定义允许上传的文件扩展名
        Hashtable extTable = new Hashtable();

        string image = ConfigurationManager.AppSettings["image"];
        string audio = ConfigurationManager.AppSettings["audio"];
        string video = ConfigurationManager.AppSettings["video"];
        string attach = ConfigurationManager.AppSettings["attach"];

        extTable.Add("image", ConfigurationManager.AppSettings["image"]);
        extTable.Add("audio", ConfigurationManager.AppSettings["audio"]);
        extTable.Add("video", ConfigurationManager.AppSettings["video"]);
        extTable.Add("attach", ConfigurationManager.AppSettings["attach"]);

        //最大文件大小
        int maxSize = 1000000;
        this.context = context;

        HttpPostedFile imgFile = context.Request.Files["imgFile"];
        if (imgFile == null)
        {
            showError("请选择文件。");
        }

        /*String dirPath = context.Server.MapPath(savePath);
        if (!Directory.Exists(dirPath))
        {
            showError("上传目录不存在。");
        }

        String dirName = context.Request.QueryString["dir"];
        if (String.IsNullOrEmpty(dirName)) {
            dirName = "image";
        }
        if (!extTable.ContainsKey(dirName)) {
            showError("目录名不正确。");
        }*/

        String fileName = imgFile.FileName;
        String fileExt = Path.GetExtension(fileName).Trim('.').ToLower();

        if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
        {
            showError("上传文件大小超过限制。");
        }

/*        if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
        {
            showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
        }*/

        string Upload = "/UploadFile";


        var FolderName = string.Empty;

        if (image.Contains(fileExt))
        {
            FolderName = "image";
        }
        else if (video.Contains(fileExt))
        {
            FolderName = "video";
        }
        else if (audio.Contains(fileExt))
        {
            FolderName = "audio";
        }
        else if (attach.Contains(fileExt))
        {
            FolderName = "attach";
        }
        else
        {
            showError("上传文件扩展名是不允许的扩展名");
        }

        Upload = $"{Upload}/{FolderName}/{DateTime.Now.ToString("yyyyMM")}";

        if(!Directory.Exists(context.Server.MapPath(Upload)))
        {
            Directory.CreateDirectory(context.Server.MapPath(Upload));
        }

        //生成文件名
        var FileName = $"{Upload}/{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.{fileExt}";

        //保存文件
        imgFile.SaveAs(context.Server.MapPath(FileName));

        Hashtable hash = new Hashtable();
        hash["error"] = 0;
        hash["url"] = FileName;
        context.Response.AddHeader("Content-Type", "text/plan; charset=UTF-8");
        context.Response.Write(JsonConvert.SerializeObject(hash));
        context.Response.End();
    }

    private void showError(string message)
    {
        Hashtable hash = new Hashtable();
        hash["error"] = 1;
        hash["message"] = message;
        context.Response.AddHeader("Content-Type", "text/plan; charset=UTF-8");
        context.Response.Write(JsonConvert.SerializeObject(hash));
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}
