using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Common;

namespace Shopping.Dal
{
    /// <summary>
    /// 通用泛型方法
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class BaseDAL<TEntity, TResult>
        where TEntity : class, new()
        where TResult : class, new()
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public List<TResult> GetAll()
        {
            ShoppingEntities db = new ShoppingEntities();
            return db.Set<TEntity>().MapToList<TEntity, TResult>();
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Create(TEntity entity)
        {
            ShoppingEntities db = new ShoppingEntities();
            db.Set<TEntity>().Add(entity);
            return db.SaveChanges();
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Create(TResult result)
        {
            ShoppingEntities db = new ShoppingEntities();
            db.Set<TEntity>().Add(result.MapTo<TEntity>());
            return db.SaveChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(TEntity entity)
        {
            ShoppingEntities db = new ShoppingEntities();
            db.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(TEntity entity)
        {
            ShoppingEntities db = new ShoppingEntities();
            db.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Deleted;
            return db.SaveChanges();
        }
    }

    public class BaseDAL
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public List<Price> GetAll()
        {
            ShoppingEntities db = new ShoppingEntities();
            return db.Set<Price>().ToList();
        }
    }
}
