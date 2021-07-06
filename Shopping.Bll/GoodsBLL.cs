using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.BuildEngine;
using Microsoft.Build.Framework;
using Shopping.Dal;
using Shopping.Model;
using System.IO;
using System.Web;

namespace Shopping.Bll
{
    public class GoodsBLL : IBasBLL<GoodsModel>
    {
        GoodsDAL goodsDAL = new GoodsDAL();

        public ResultModel Create(GoodsModel TModel)
        {
            int count = goodsDAL.Create(TModel);
            if(count > 0)
            {
                return new ResultModel { ErrorCode = 0 };
            }
            else
            {
                return new ResultModel { ErrorCode = 1, Info = "添加失败" };
            }
        }

        public ResultModel Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResultModel Delete(int[] idList)
        {
            throw new NotImplementedException();
        }

        public List<GoodsModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public GoodsModel GetModel(int id)
        {
            return goodsDAL.GetModel(id);
        }

        public Tuple<int, int, List<GoodsModel>> GetPageDataTuple(int pageSize, int PageIndex, string Keywords)
        {
            return goodsDAL.GetPageDataTuple(pageSize, PageIndex, Keywords);
        }

        public ResultModel Update(GoodsModel Model)
        {
            try
            {
                var model = GetModel(Model.GoodsID);

                //上传了新的图片
                if(model.GoodsPic != Model.GoodsPic)
                {
                    string path = HttpContext.Current.Server.MapPath(model.GoodsPic);

                    //删除旧的图片
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }

                goodsDAL.Update(Model);

                return new ResultModel { ErrorCode = 0 };
            }
            catch (Exception e)
            {
                return new ResultModel { ErrorCode = 1, Info = $"更新异常,异常信息：{e.Message}" };
            }
        }
    }
}
