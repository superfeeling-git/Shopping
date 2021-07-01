using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Model;

namespace Shopping.Dal
{
    public class GoodsCategoryDAL
    {
        public List<GoodsCategoryModel> GetAll()
        {
            ShoppingEntities db = new ShoppingEntities();
            return db.GoodsCategory.ToList().Select(m => new GoodsCategoryModel
            {
                CategoryID = m.CategoryID,
                CategoryName = m.CategoryName
            }).ToList();
        }
    }
}
