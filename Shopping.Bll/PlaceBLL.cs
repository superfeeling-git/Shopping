using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Model;
using Shopping.Dal;

namespace Shopping.Bll
{
    public class PlaceBLL
    {
        PlaceDal placeDal = new PlaceDal();

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public List<PlaceModel> GetAll()
        {
            return placeDal.GetAll();
        }
    }
}
