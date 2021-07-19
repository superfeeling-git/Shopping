using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// 出租房屋
    /// </summary>
    /// <param name="person">承租人</param>
    /// <param name="saleTime">出租时间</param>
    /// <param name="Price">出租价格</param>
    /// <returns></returns>
    //public delegate int Sales(Person person, DateTime saleTime, int Price);

    class Program
    {
        static void Main(string[] args)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.qq.com");

            smtpClient.UseDefaultCredentials = false;

            smtpClient.Credentials = new System.Net.NetworkCredential("515856410@qq.com", "jptpaievsfuocaba");

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            //邮件消息对象
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress("515856410@qq.com");

            mailMessage.To.Add("515856410@qq.com");

            mailMessage.To.Add("1374527101@qq.com");

            mailMessage.To.Add("1530092038@qq.com");

            mailMessage.Subject = "HELLO";

            mailMessage.Body = "<span style='color:red'>邮件正文</span>";

            mailMessage.IsBodyHtml = true;

            mailMessage.BodyEncoding = Encoding.UTF8;

            mailMessage.CC.Add("672341672@qq.com");

            smtpClient.Send(mailMessage);

            Console.ReadLine();
        }
    }

    #region MyRegion
    /*    public class MyHouse
        {
            public void WriteText(Person person)
            {
                Console.WriteLine($"承租人信息：{person.Name}");
            }


            public int Test()
            {
                return int.MinValue;
            }

            /// <summary>
            /// 出租房屋
            /// </summary>
            /// <param name="person">承租人</param>
            /// <param name="saleTime">出租时间</param>
            /// <param name="Price">出租价格</param>
            /// <returns></returns>
            public int Sales(Person person, DateTime saleTime, int Price)
            {
                //........
                //业务逻辑

                Console.WriteLine($"承租人信息：{person.Name}");

                Console.WriteLine($"出租时间：{saleTime.ToLongDateString()}");

                Console.WriteLine($"价格：{Price}");

                return int.MaxValue;
            }
        }
    */
    #endregion

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
