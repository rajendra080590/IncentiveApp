using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FBMICService.Utility
{
    public interface IEmailSender
    {
        Task<string> SendEmailNotification(string toMail, string ccMail, string subject, string body, string bcc = null);
    }
}
