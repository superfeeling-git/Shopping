using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Model;
using Shopping.Dal;

namespace Shopping.Bll
{
    public class OutMaterialBLL
    {
        OutMaterialDAL outMaterialDAL = new OutMaterialDAL();

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public List<OutMaterialModel> GetAll()
        {
            return outMaterialDAL.GetAll();
        }
    }
}
