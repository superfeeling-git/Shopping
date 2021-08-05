using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Model
{
    public class UserAddressModel
    {
        public int AddressID { get; set; }
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
