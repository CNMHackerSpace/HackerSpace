using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    public interface IEmailSenderService:IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);

    }
}
