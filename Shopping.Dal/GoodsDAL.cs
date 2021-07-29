using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Model;
using System.Data.Entity;
using System.Linq.Dynamic.Core;
using EntityFramework.Extensions;
using System.Linq.Expressions;
using System.Data.Entity.Core.Objects;
using Newtonsoft.Json;

namespace Shopping.Dal
{
    public class GoodsDAL
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
        /// 获取新品
        /// </summary>
        /// <returns></returns>
        public List<GoodsModel> GetGoods(int Top, Expression<Func<Goods, bool>> expression)
        {
            ShoppingEntities db = new ShoppingEntities();

            var list = db.Goods.Where(expression).Take(Top).ToList().MapToList<Goods, GoodsModel>();

            return list;
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

        /*
        public Tuple<int, int, List<GoodsModel>> GetPageDataTuple<TKey>(GoodsQueryModel goodsQuery, Expression<Func<Goods, TKey>> keySelector, int pageSize, int PageIndex)
        {
            //C#基础    泛型、反射、委托、多线程、Async/await（异步方法）

            ShoppingEntities db = new ShoppingEntities();

            var list = db.Goods.AsQueryable();

            #region 条件判断

            if (!string.IsNullOrEmpty(goodsQuery.Keywords))
            {
                list = list.Where(m => m.GoodsName.Contains(goodsQuery.Keywords));
            }

            if (goodsQuery.CategoryID != null)
            {
                list = list.Where(m => m.CategoryID == goodsQuery.CategoryID);
            }

            if (goodsQuery.PlaceID != null)
            {
                list = list.Where(m => m.PlaceID == goodsQuery.PlaceID);
            }

            if (goodsQuery.OutID != null)
            {
                list = list.Where(m => m.OutMaterialID == goodsQuery.OutID);
            }

            if (goodsQuery.RangeID != null)
            {
                list = list.Where(m => m.RangeID == goodsQuery.RangeID);
            }

            #endregion

            list = list.OrderBy(keySelector) ;

            //总条数
            var TotalCount = list.Count();

            //总页数
            var PageCount = (int)Math.Ceiling(TotalCount * 1.0 / pageSize);

            //返回数据
            return new Tuple<int, int, List<GoodsModel>>(item1: TotalCount, item2: PageCount, item3: Goodslist);
        }
        */

        /// <summary>
        /// 多条件排序
        /// </summary>
        /// <param name="goodsQuery"></param>
        /// <param name="Field"></param>
        /// <param name="OrderBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public Tuple<int, int, List<GoodsModel>> GetPageDataTuple(GoodsQueryModel goodsQuery, string Field, string OrderBy, int pageSize, int PageIndex)
        {
            ShoppingEntities db = new ShoppingEntities();

            var list = db.Goods.AsQueryable();

            #region 条件判断

            if (!string.IsNullOrEmpty(goodsQuery.Keywords))
            {
                list = list.Where(m => m.GoodsName.Contains(goodsQuery.Keywords));
            }

            if (goodsQuery.CategoryID != null)
            {
                list = list.Where(m => m.CategoryID == goodsQuery.CategoryID);
            }

            if (goodsQuery.PlaceID != null)
            {
                list = list.Where(m => m.PlaceID == goodsQuery.PlaceID);
            }

            if (goodsQuery.OutID != null)
            {
                list = list.Where(m => m.OutMaterialID == goodsQuery.OutID);
            }

            if (goodsQuery.RangeID != null)
            {
                list = list.Where(m => m.RangeID == goodsQuery.RangeID);
            }

            #endregion

            var Goodslist = list.OrderBy($"{Field} {OrderBy}").Page(PageIndex, pageSize).MapToList<Goods, GoodsModel>();

            //总条数
            var TotalCount = list.Count();

            //总页数
            var PageCount = (int)Math.Ceiling(TotalCount * 1.0 / pageSize);

            //返回数据
            return new Tuple<int, int, List<GoodsModel>>(item1: TotalCount, item2: PageCount, item3: Goodslist);
        }

        /// <summary>
        /// 通过存储过程调用分页
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public Tuple<int, int, List<GoodsModel>> GetPageDataTuple(int pageSize, int PageIndex)
        {
            ShoppingEntities db = new ShoppingEntities();

            ObjectParameter totalcount = new ObjectParameter("TotalCount", typeof(int));

            ObjectParameter pagecount = new ObjectParameter("PageCount", typeof(int));

            var list = db.P_GoodsPage(PageIndex, pageSize, totalcount, pagecount);

            //序列化
            var json = JsonConvert.SerializeObject(list);

            return new Tuple<int, int, List<GoodsModel>>
                (
                item1: (int)totalcount.Value,
                item2: (int)pagecount.Value,
                item3: JsonConvert.DeserializeObject<List<GoodsModel>>(json)
                );
        }

        public Tuple<int, int, List<GoodsModel>> GetPageDataTuple(int pageSize, int PageIndex, QueryBaseModel queryBase)
        {
            ShoppingEntities db = new ShoppingEntities();

            var list = db.GoodsCategory.Join(db.Goods, a => a.CategoryID, b => b.CategoryID, (a, b) => new GoodsModel
            {
                CategoryID = a.CategoryID,
                CategoryName = a.CategoryName,
                Details = b.Details,
                GoodsName = b.GoodsName,
                GoodsPic = b.GoodsPic,
                IsShow = b.IsShow,
                Price = b.Price,
                Stock = b.Stock,
                CreateTime = b.CreateTime,
                GoodsID = b.GoodsID,
                IsHome = b.IsHome,
                IsHot = b.IsHot,
                IsNew = b.IsNew,
                IsToday = b.IsToday
            }).Where(m => m.IsShow);

            if (!string.IsNullOrWhiteSpace(queryBase.Keywords))
            {
                list = list.Where(m => m.GoodsName.Contains(queryBase.Keywords));
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
