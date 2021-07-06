using System;
using System.Collections.Generic;

namespace Shopping.Dal
{
    public interface IBaseDAL<T>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="TModel"></param>
        /// <returns></returns>
        int Create(T TModel);
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="Keywords"></param>
        /// <returns></returns>

        Tuple<int, int, List<T>> GetPageDataTuple(int pageSize, int PageIndex, string Keywords);
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        T GetModel(int id);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        int Update(T Model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(int id);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        int Delete(int[] idList);
    }
}