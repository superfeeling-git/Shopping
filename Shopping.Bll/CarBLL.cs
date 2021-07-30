using Shopping.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Dal;

namespace Shopping.Bll
{
    /// <summary>
    /// 购物车业务逻辑层（登录、未登录）两种状态
    /// </summary>
    public class CarBLL : ICar
    {
        SessionCar sessionCar = new SessionCar();

        CarDAL carDAL = new CarDAL();

        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="carModel"></param>
        /// <returns></returns>
        public List<CarModel> AddCar(CarModel carModel)
        {
            List<CarModel> list;

            if (UserContext.IsLogin)
            {
                carModel.UserID = UserContext.GetUser.UserID;

                var carList = GetCar();

                var carGoods = carList.FirstOrDefault(m => m.UserID == UserContext.GetUser.UserID && m.GoodsID == carModel.GoodsID);

                //数据库已经存在
                if (carGoods != null)
                {
                    carModel.UserID = UserContext.GetUser.UserID;

                    carModel.BuyCount += carGoods.BuyCount;

                    list = carDAL.UpdateCar(carModel);
                }
                else
                {
                    list = carDAL.AddCar(carModel);
                }                
            }
            else
            {
                list = sessionCar.AddCar(carModel);
            }
            return list;
        }

        /// <summary>
        /// 获取购物车
        /// </summary>
        /// <returns></returns>
        public List<CarModel> GetCar()
        {
            return UserContext.IsLogin ? carDAL.GetCar(UserContext.GetUser.UserID) : sessionCar.GetCar();
        }

        /// <summary>
        /// 更新购物车
        /// </summary>
        /// <param name="carModel"></param>
        /// <returns></returns>
        public List<CarModel> UpdateCar(CarModel carModel)
        {
            List<CarModel> list;

            if (UserContext.IsLogin)
            {
                carModel.UserID = UserContext.GetUser.UserID;
                list = carDAL.UpdateCar(carModel);
            }
            else
            {
                list = sessionCar.UpdateCar(carModel);
            }
            return list;
        }


        /// <summary>
        /// 删除购物车
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CarModel> DelCar(int id)
        {
            List<CarModel> list;

            if (UserContext.IsLogin)
            {                
                list = carDAL.DelCar
                (
                new CarModel 
                {
                    UserID = UserContext.GetUser.UserID,
                    GoodsID = id
                });
            }
            else
            {
                list = sessionCar.DelCar(id);
            }
            return list;
        }

        /// <summary>
        /// 批量删除购物车
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public List<CarModel> BulkDelCar(int[] idList)
        {
            List<CarModel> list;

            if (UserContext.IsLogin)
            {
                list = carDAL.BulkDelCar(idList, UserContext.GetUser.UserID);
            }
            else
            {
                list = sessionCar.BulkDelCar(idList);
            }
            return list;
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <returns></returns>
        public bool ClearCar()
        {
            try
            {
                if (UserContext.IsLogin)
                {
                    carDAL.ClearCar(UserContext.GetUser.UserID);
                }
                else
                {
                    sessionCar.ClearCar();
                }
                return true;
            }
            catch (Exception)
            {
                //日志记录处理
                //NLOG
                return false;
            }
        }
    }
}
