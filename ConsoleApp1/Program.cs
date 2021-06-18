using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> str1 = new List<string> { "a", "b", "c" };
            List<string> str2 = new List<string>();

            for (int i = 0; i < str1.Count(); i++)
            {
                if(i == 2)
                {
                    str2.Add("d");
                }
                else
                {
                    str2.Add(str1[i]);
                }
            }

            foreach (var item in str2)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
    }
}
