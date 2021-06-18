//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shopping.Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShoppingOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShoppingOrder()
        {
            this.OrderGoods = new HashSet<OrderGoods>();
        }
    
        public int OrderID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string OrderNum { get; set; }
        public Nullable<System.DateTime> OrderTime { get; set; }
        public string FullName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Nullable<byte> OrderStatus { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderGoods> OrderGoods { get; set; }
        public virtual User User { get; set; }
    }
}
