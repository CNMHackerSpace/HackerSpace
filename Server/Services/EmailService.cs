// Copyright (c) 2025. All rights reserved.

using System.Net;
using System.Net.Mail;
using Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Server.Services
{
    /// <summary>
    /// Service implementation for sending emails using SMTP.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration instance used to retrieve email settings.</param>
        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <inheritdoc />
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpHost = this.configuration["Email:SmtpHost"];
            var smtpPort = int.Parse(this.configuration["Email:SmtpPort"] ?? "587");
            var smtpUser = this.configuration["Email:SmtpUser"];
            var smtpPassword = this.configuration["Email:SmtpPassword"];
            var fromAddress = this.configuration["Email:FromAddress"] ?? smtpUser;

            if (string.IsNullOrEmpty(smtpHost) || string.IsNullOrEmpty(smtpUser))
            {
                // Email not configured - log instead
                Console.WriteLine($"Email (not sent - no config): To={to}, Subject={subject}");
                return;
            }

            if (string.IsNullOrWhiteSpace(fromAddress))
            {
                throw new InvalidOperationException("Email:FromAddress or Email:SmtpUser must be configured.");
            }

            using var message = new MailMessage
            {
                From = new MailAddress(fromAddress),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            message.To.Add(to);

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPassword),
                EnableSsl = true,
            };

            await client.SendMailAsync(message);
        }
    }
}
