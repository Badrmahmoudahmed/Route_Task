using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Services.Contract
{
    public interface IEmailSender
    {
        Task SendMailAsync(string to, string subject, string body);
    }
}
