using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Shopping.Dal;
using Shopping.Model;
using Newtonsoft.Json;
using AutoMapper;
using Shopping.Bll;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Text.RegularExpressions;
using EntityFramework.Extensions;
using System.Data.Entity.Core.Objects;
using Shopping.Common;
using Spire;
using Spire.Xls;
using System.Drawing;
using System.Collections.Specialized;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            NameValueCollection a = ConfigurationManager.AppSettings;

            SqlParameter parm = new SqlParameter("@ID", DBNull.Value);



            PriceDAL dAL = new PriceDAL();

            dAL.Create(new PriceModel { RangeName = "666", RangeOrder  = 101  });


            Console.ReadLine();
        }
    }


    public partial class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public partial class Person
    {
        public string Birthday { get; set; }
    }
}