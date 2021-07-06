using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Model;
using System.Data.Entity;
using System.Linq.Dynamic.Core;
using EntityFramework.Extensions;

namespace Shopping.Dal
{
    public class GoodsDAL : IBaseDAL<GoodsModel>
    {
        public int Create(GoodsModel TModel)
        {
            ShoppingEntities db = new ShoppingEntities();

            

            Goods goods = new Goods
            {
                CategoryID = TModel.CategoryID,
                CreateTime = DateTime.Now,
                Details = TModel.Details,
                GoodsName = TModel.GoodsName,
                GoodsPic = TModel.GoodsPic,
                IsShow = TModel.IsShow,
                Price = TModel.Price,
                Stock = TModel.Stock
            };

            db.Entry<Goods>(goods).State = EntityState.Added;

            //lkasfjlakhdfkashdklfads

            /*db.Goods.Add(goods);

            db.Set<Goods>().Add(goods);*/

            return db.SaveChanges();
        }

        public int Delete(int id)
        {
            ShoppingEntities db = new ShoppingEntities();
            return db.Goods.Where(m => m.GoodsID == id).Delete();
        }

        public int Delete(int[] idList)
        {
            throw new NotImplementedException();
        }

        public List<GoodsModel> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GoodsModel GetModel(int id)
        {
            ShoppingEntities db = new ShoppingEntities();
            return db.Goods.Where(m => m.GoodsID == id).Select(m => new GoodsModel {
                CategoryID = m.CategoryID,
                Details = m.Details,
                GoodsName = m.GoodsName,
                GoodsPic = m.GoodsPic,
                IsShow = m.IsShow,
                Price = m.Price,
                Stock = m.Stock,
                CreateTime = m.CreateTime,
                GoodsID = m.GoodsID
            }).FirstOrDefault();
        }

        public Tuple<int, int, List<GoodsModel>> GetPageDataTuple(int pageSize, int PageIndex, string Keywords)
        {
            ShoppingEntities db = new ShoppingEntities();

            var list = db.GoodsCategory.Join(db.Goods, a => a.CategoryID, b => b.CategoryID, (a, b) => new GoodsModel {
                CategoryID = a.CategoryID,
                CategoryName = a.CategoryName,
                Details = b.Details,
                GoodsName = b.GoodsName,
                GoodsPic = b.GoodsPic,
                IsShow = b.IsShow,
                Price = b.Price,
                Stock = b.Stock, 
                CreateTime = b.CreateTime, 
                GoodsID = b.GoodsID
            }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                list = list.Where(m => m.GoodsName.Contains(Keywords));
            }

            //分页数据
            var Goodslist = list.OrderBy(m => m.GoodsID).Page(PageIndex,pageSize).ToList();

            //总条数
            var TotalCount = list.Count();

            //总页数
            var PageCount = (int)Math.Ceiling(TotalCount * 1.0 / pageSize);

            return new Tuple<int, int, List<GoodsModel>>(item1: TotalCount, item2: PageCount, item3: Goodslist);
        }

        public int Update(GoodsModel Model)
        {
            ShoppingEntities db = new ShoppingEntities();

            return db.Goods.Where(m=>m.GoodsID == Model.GoodsID).Update(m => new Goods {
                CategoryID = Model.CategoryID,
                Details = Model.Details,
                GoodsName = Model.GoodsName,
                GoodsPic = Model.GoodsPic,
                IsShow = Model.IsShow,
                Price = Model.Price,
                Stock = Model.Stock
            });
        }
    }
}
