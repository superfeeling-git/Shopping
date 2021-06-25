using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Model
{
    public class ResultModel
    {
        /// <summary>
        /// 0:成功
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Info { get; set; }
    }
}
