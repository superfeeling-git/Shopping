using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Model;

namespace Shopping.Dal
{
    public class UserDAL
    {
        public int Create(UserModel userModel)
        {
            ShoppingEntities db = new ShoppingEntities();
            db.User.Add(new User {
                Birthday = userModel.Birthday, 
                HandPhone = userModel.HandPhone, 
                FullName = userModel.FullName
            });
            return db.SaveChanges();
        }
    }
}
