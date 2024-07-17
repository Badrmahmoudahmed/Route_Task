using Microsoft.Extensions.Configuration;
using OrderManagement.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OrderMangement.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendMailAsync(string to, string subject, string body)
        {
            var mailmsg = new MailMessage();
            var masilsender = new MailAddress(_configuration["Smtp:UserName"]);
            mailmsg.Subject = subject;
            mailmsg.Body = body;
            mailmsg.To.Add(to);
            mailmsg.From = masilsender;

            var smtpclient = new SmtpClient(_configuration["Smtp:Host"], int.Parse(_configuration["Smtp:Port"]));

            smtpclient.Credentials = new NetworkCredential(_configuration["Smtp:UserName"], _configuration["Smtp:Password"]);
            smtpclient.EnableSsl = bool.Parse(_configuration["Smtp:EnableSsl"]);

           await smtpclient.SendMailAsync(mailmsg);
        }
    }
}
