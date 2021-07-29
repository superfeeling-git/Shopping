using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Model;
using Shopping.Common;

namespace Shopping.Dal
{
    /// <summary>
    /// 字典DAL
    /// </summary>
    public class DictDAL
    {
        /// <summary>
        /// 根据字典类型获取字典项
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>

        public List<DictModel> GetListForType(int type)
        {
            ShoppingEntities db = new ShoppingEntities();
            return db.Dict.Where(m => m.DictType == type).ToList().MapToList<Dict, DictModel>();
        }
    }
}
