using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Shopping.Model;
using System.Configuration;

namespace Shopping.Dal
{

    public class GoodsDALForADO : IBaseDAL<GoodsModel>
    {
        private string ConnStr = ConfigurationManager.ConnectionStrings["ShopContext"].ConnectionString;

        public int Create(GoodsModel TModel)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                string Sql = "INSERT INTO Goods VALUES(@CategoryID, @GoodsName, @Price, @Stock, @IsShow, @Details, @GoodsPic, @CreateTime)";

                cmd.CommandText = Sql;

                SqlParameter[] sqlParameter = new SqlParameter[] {
                    new SqlParameter("@CategoryID",SqlDbType.Int),
                    new SqlParameter("@GoodsName",SqlDbType.NVarChar,50),
                    new SqlParameter("@Price",SqlDbType.Money),
                    new SqlParameter("@Stock",SqlDbType.Int),
                    new SqlParameter("@IsShow",SqlDbType.Bit),
                    new SqlParameter("@Details",SqlDbType.NVarChar,-1),
                    new SqlParameter("@GoodsPic",SqlDbType.NVarChar,50),
                    new SqlParameter("@CreateTime",SqlDbType.DateTime)
                };

                sqlParameter[0].Value = TModel.CategoryID;
                sqlParameter[1].Value = TModel.GoodsName;
                sqlParameter[2].Value = TModel.Price;
                sqlParameter[3].Value = TModel.Stock;
                sqlParameter[4].Value = TModel.IsShow;
                sqlParameter[5].Value = TModel.Details;
                sqlParameter[6].Value = TModel.GoodsPic;
                sqlParameter[7].Value = TModel.CreateTime;

                cmd.Parameters.AddRange(sqlParameter);

                return cmd.ExecuteNonQuery();
            }
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Delete(int[] idList)
        {
            throw new NotImplementedException();
        }

        public List<GoodsModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public GoodsModel GetModel(int id)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, int, List<GoodsModel>> GetPageDataTuple(int pageSize, int PageIndex, string Keywords)
        {
            throw new NotImplementedException();
        }

        public int Update(GoodsModel Model)
        {
            throw new NotImplementedException();
        }
    }
}
