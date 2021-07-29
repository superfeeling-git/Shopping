using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Model
{
    public class PlaceModel
    {
        public int PlaceID { get; set; }
        public string PlaceName { get; set; }
        public Nullable<int> PlaceOrder { get; set; }
    }
}
