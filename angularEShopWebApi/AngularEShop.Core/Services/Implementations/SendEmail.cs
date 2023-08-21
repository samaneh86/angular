using AngularEShop.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.Core.Services.Implementations
{
    public class SendEmail:IEmailSender
    {
       
        public void Send(string to,string subject , string body)
        {
            var defaultEmail="samaneh.vafaeenezhad@gmail.com";
            var mail = new MailMessage();
            mail.From = new MailAddress(defaultEmail, "فروشگاه انگولار");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;


            var smtpServer = new SmtpClient();
            smtpServer.Host = "smtp.gmail.com";
            
            smtpServer.Port =587;
            smtpServer.Credentials = new NetworkCredential(defaultEmail, "vmbphcxyojtdjwpg");
            smtpServer.EnableSsl = true;
             smtpServer.Send(mail);
        }
    }
}
