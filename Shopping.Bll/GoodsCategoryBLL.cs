using Shopping.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Dal;

namespace Shopping.Bll
{
    public class GoodsCategoryBLL
    {
        GoodsCategoryDAL GoodsCategoryDAL = new GoodsCategoryDAL();

        public List<GoodsCategoryModel> GetAll()
        {
            return GoodsCategoryDAL.GetAll();
        }
    }
}
