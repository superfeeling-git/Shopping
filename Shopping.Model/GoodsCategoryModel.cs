using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Model
{
    /// <summary>
    /// 说明 商品分类表
    /// </summary>
    [Serializable]
    public class GoodsCategoryModel
    {
        #region 公共属性
        ///<Summary>
        /// 主键
        ///</Summary>
        public int CategoryID { get; set; }
        ///<Summary>
        /// 分类名称
        ///</Summary>
        public string CategoryName { get; set; }
        ///<Summary>
        /// 父节点ID
        ///</Summary>
        public int ParentID { get; set; }
        ///<Summary>
        /// 节点路径
        ///</Summary>
        public string ParentPath { get; set; }
        /// <summary>
        /// 多个商品
        /// </summary>
        public List<GoodsModel> GoodsModel { get; set; }
        #endregion
    }
}
