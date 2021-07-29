﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Model;
using Shopping.Dal;

namespace Shopping.Bll
{
    public class DictBLL
    {
        DictDAL dictDAL = new DictDAL();

        public List<DictModel> GetListForType(int type)
        {
            return dictDAL.GetListForType(type);
        }
    }
}
