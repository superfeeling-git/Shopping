using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Dal;
using Shopping.Model;

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
            throw new NotImplementedException();
        }

        public Tuple<int, int, List<GoodsModel>> GetPageDataTuple(int pageSize, int PageIndex, string Keywords)
        {
            return goodsDAL.GetPageDataTuple(pageSize, PageIndex, Keywords);
        }

        public ResultModel Update(GoodsModel userModel)
        {
            throw new NotImplementedException();
        }
    }
}
