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
                UserName = userModel.UserName,
                Password = userModel.Password,                
                Birthday = userModel.Birthday, 
                HandPhone = userModel.HandPhone, 
                FullName = userModel.FullName,
                CreateTime = userModel.CreateTime,
                IsLock = userModel.IsLock,
                LastLoginIP = userModel.LastLoginIP,
                LastLoginTime = userModel.LastLoginTime,
                Sex = userModel.Sex
            });
            return db.SaveChanges();
        }

        /// <summary>
        /// 根据用户名判断是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool IsExists(string UserName)
        {
            ShoppingEntities db = new ShoppingEntities();

            return db.User.Any(m => m.UserName == UserName);
        }

        /// <summary>
        /// 根据用户名判断是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool IsExists(string UserName, int UserID)
        {
            ShoppingEntities db = new ShoppingEntities();

            return db.User.Any(m => m.UserName == UserName && m.UserID != UserID);
        }


        /// <summary>
        /// 根据用户名判断是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public UserModel IsExistsForUser(string UserName)
        {
            ShoppingEntities db = new ShoppingEntities();

            UserModel user = null;

            var entity = db.User.FirstOrDefault(m => m.UserName == UserName);

            if(entity != null)
            {
                user = new UserModel();
                user.IsLock = entity.IsLock;
                user.UserName = entity.UserName;
                user.LastLoginTime = entity.LastLoginTime;
                user.LastLoginIP = entity.LastLoginIP;
                user.Birthday = entity.Birthday;
                user.Password = entity.Password;
            }

            return user;
        }

        /// <summary>
        /// 第一种方法
        /// </summary>
        /// <returns></returns>
        public List<UserModel> GetPageData(int pageSize,int PageIndex,ref int PageCount,ref int TotalCount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 第二种方法
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public PageModel<UserModel> GetPageData(int pageSize, int PageIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="Keywords">关键字</param>
        /// <returns></returns>
        public Tuple<int, int, List<UserModel>> GetPageDataTuple(int pageSize, int PageIndex, string Keywords)
        {
            ShoppingEntities db = new ShoppingEntities();

            var list = db.User.AsQueryable();

            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                list = list.Where(m => m.UserName.Contains(Keywords) || m.FullName.Contains(Keywords) || m.HandPhone.Contains(Keywords));
            }

            //......条件


            //分页数据
            var Userlist = list.OrderBy(m=>m.UserID).Skip((PageIndex - 1) * pageSize).Take(pageSize)
                .Select(m => new UserModel {
                    UserName = m.UserName,
                    Birthday = m.Birthday,
                    CreateTime = (DateTime)m.CreateTime,
                    IsLock = m.IsLock,
                    FullName = m.FullName,
                    HandPhone = m.HandPhone,
                    Sex = (bool)m.Sex,
                    UserID = m.UserID
                }).ToList();


            //总条数
            var TotalCount = list.Count();

            //总页数
            var PageCount = (int)Math.Ceiling(TotalCount * 1.0 / pageSize);

            return new Tuple<int, int, List<UserModel>>( item1: TotalCount, item2: PageCount, item3: Userlist);
        }

        /// <summary>
        /// 返回所有数据
        /// </summary>
        /// <returns></returns>
        public List<UserModel> GetAll()
        {
            ShoppingEntities db = new ShoppingEntities();

            //数据库里的集合
            return db.User.ToList().Select(m => new UserModel {
                UserName = m.UserName,
                UserID = m.UserID
            }).ToList();
        }

        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserModel GetUser(int id)
        {
            ShoppingEntities db = new ShoppingEntities();
            var user = db.User.Find(id);
            return new UserModel {
                Birthday = user.Birthday,
                CreateTime = (DateTime)user.CreateTime,
                FullName = user.FullName,
                HandPhone = user.HandPhone,
                IsLock = user.IsLock,
                Password = user.Password,
                Sex = (bool)user.Sex,
                UserID = user.UserID,
                UserName = user.UserName
            };
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public int Update(UserModel userModel)
        {
            ShoppingEntities db = new ShoppingEntities();

            var user = db.User.Find(userModel.UserID);
            user.Birthday = userModel.Birthday;
            user.FullName = userModel.FullName;
            user.HandPhone = userModel.HandPhone;
            user.Password = string.IsNullOrWhiteSpace(userModel.Password) ? user.Password : userModel.Password;
            user.Sex = userModel.Sex;
            user.UserName = userModel.UserName;

            return db.SaveChanges();
        }

        public int Delete(int id)
        {
            ShoppingEntities db = new ShoppingEntities();
            var user = db.User.Find(id);
            db.User.Remove(user);
            return db.SaveChanges();
        }

        public int Delete(string ids)
        {
            ShoppingEntities db = new ShoppingEntities();

            string[] arr = ids.Split(',');

            foreach (var item in arr)
            {
                var list = db.User.Find((Convert.ToInt32(item)));
                db.User.Remove(list);
            }
            return db.SaveChanges();
        }

        public int Delete(int[] idList)
        {
            ShoppingEntities db = new ShoppingEntities();

            var list = db.User.Where(m => idList.Any(a => a == m.UserID));

            var list1 = db.User.Where(m => idList.Contains(m.UserID));

            foreach (var item in list)
            {
                db.User.Remove(item);
            }

            return db.SaveChanges();
        }
    }
}