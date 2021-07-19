using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Shopping.Common
{
    public class FromEmail
    {
        /// <summary>
        /// SMTP服务器
        /// </summary>
        public string smtp { get; set; }
        /// <summary>
        /// 发件人账号
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }

    public class EmailHelper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="fromEmail"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="to"></param>
        /// <param name="cc"></param>
        public static bool Send(FromEmail fromEmail, string subject, string body, List<string> to,List<string> cc = null)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(fromEmail.smtp);

                smtpClient.UseDefaultCredentials = false;

                smtpClient.Credentials = new System.Net.NetworkCredential(fromEmail.From, fromEmail.Password);

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                //邮件消息对象
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(fromEmail.From);

                foreach (var item in to)
                {
                    mailMessage.To.Add(item);
                }

                mailMessage.Subject = subject;

                mailMessage.Body = body;

                mailMessage.IsBodyHtml = true;

                mailMessage.BodyEncoding = Encoding.UTF8;

                if(cc != null)
                {
                    foreach (var item in cc)
                    {
                        mailMessage.CC.Add(item);
                    }
                }

                smtpClient.Send(mailMessage);

                return true;

            }
            catch (Exception e)
            {
                logger.Error(e);
                return false;
            }
        }
    }
}
