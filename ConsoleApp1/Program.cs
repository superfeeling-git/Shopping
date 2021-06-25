using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Shopping.Dal;

namespace ConsoleApp1
{
    public delegate string myDele(string[] arr);

    class Program
    {
        static void Main(string[] args)
        {
            #region MyRegion
            /*Program program = new Program();

            myDele myDele = new myDele(program.StringArr);

            string result = myDele(new string[] {"c#高级开发", "JavaScript前端开发" });

            Console.WriteLine(result);*/

            /*Func<string[], string> func0 = new Func<string[], string>(m =>
            {
                return string.Join(",", m.Select(a => $"《{a.ToUpper()}》"));
            });

            Func<string[], string> func1 = m => {
                return string.Join(",", m.Select(a => $"《{a.ToUpper()}》"));
            };*/

            #endregion

            /*Func<string[], string> func2 = m => string.Join(",", m.Select(a => $"《{a.ToUpper()}》"));

            *//*Console.WriteLine(func2(args));*//*

            var r = Math.Ceiling(8m / 3);

            Console.WriteLine(r);*/

            DateTime? date = DateTime.Now;

            string str = string.Format("{0:yyyy-MM-dd}", date);
            Console.WriteLine(str);

            Console.ReadLine();
        }

        public string StringArr(string[] arr)
        {
            #region MyRegion
            //string[] str_abc = new string[] { "1","2","3"};

            //第一种方法
            /*List<string> strList = new List<string>();

            foreach (var item in arr)
            {
                strList.Add(item.ToUpper());
            }

            foreach (var item in strList)
            {
                Console.WriteLine(item);
            }*/

            //第二方法
            /*string[] intarr = Array.ConvertAll(arr, m => m.ToUpper());

            foreach (var item in intarr)
            {
                Console.WriteLine($"值:{item},类型：{item.GetType()}");
            }*/

            #endregion
            //第三种方法
            /*var list = arr.Select(m => $"《{m.ToUpper()}》");

            var str = String.Join(",", list);

            Console.WriteLine(str);*/

            return string.Join(",", arr.Select(m => $"《{m.ToUpper()}》"));
        }
    }
}
