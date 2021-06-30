using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Model
{
    /// <summary>
    /// 说明 商品表
    /// </summary>
    [Serializable]
    public class GoodsModel
    {
        #region 公共属性
        ///<Summary>
        /// 商品ID
        ///</Summary>
        public int GoodsID { get; set; }
        ///<Summary>
        /// 主键
        ///</Summary>
        public int CategoryID { get; set; }
        ///<Summary>
        /// 商品名称
        ///</Summary>
        public string GoodsName { get; set; }
        ///<Summary>
        /// 商品价格
        ///</Summary>
        public decimal Price { get; set; }
        ///<Summary>
        /// 商品库存
        ///</Summary>
        public int Stock { get; set; }
        ///<Summary>
        /// 是否上架
        ///</Summary>
        public bool IsShow { get; set; }
        ///<Summary>
        /// 商品介绍
        ///</Summary>
        public string Details { get; set; }
        ///<Summary>
        /// 商品图片
        ///</Summary>
        public string GoodsPic { get; set; }
        ///<Summary>
        /// 添加时间
        ///</Summary>
        public DateTime CreateTime { get; set; }
        #endregion
    }
}
