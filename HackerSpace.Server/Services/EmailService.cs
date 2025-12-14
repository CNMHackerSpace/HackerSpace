// Copyright (c) 2025. All rights reserved.

using System.Net;
using System.Net.Mail;
using HackerSpace.Shared.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HackerSpace.Server.Services
{
    /// <summary>
    /// Service implementation for sending emails using SMTP.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc />
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpHost = _configuration["Email:SmtpHost"];
            var smtpPort = int.Parse(_configuration["Email:SmtpPort"] ?? "587");
            var smtpUser = _configuration["Email:SmtpUser"];
            var smtpPassword = _configuration["Email:SmtpPassword"];
            var fromAddress = _configuration["Email:FromAddress"] ?? smtpUser;

            if (string.IsNullOrEmpty(smtpHost) || string.IsNullOrEmpty(smtpUser))
            {
                // Email not configured - log instead
                Console.WriteLine($"Email (not sent - no config): To={to}, Subject={subject}");
                return;
            }

            using var message = new MailMessage
            {
                From = new MailAddress(fromAddress),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(to);

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPassword),
                EnableSsl = true
            };

            await client.SendMailAsync(message);
        }
    }
}