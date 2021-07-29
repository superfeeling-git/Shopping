using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Model;
using Shopping.Dal;

namespace Shopping.Bll
{
    public class PriceBLL
    {
        PriceDAL priceDAL = new PriceDAL();

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public List<PriceModel> GetAll()
        {
            return priceDAL.GetAll();
        }
    }
}
