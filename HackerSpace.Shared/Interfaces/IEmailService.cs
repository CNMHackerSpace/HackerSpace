// Copyright (c) 2025. All rights reserved.

namespace HackerSpace.Shared.Interfaces
{
    /// <summary>
    /// Service interface for sending emails.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="to">Recipient email address.</param>
        /// <param name="subject">Email subject.</param>
        /// <param name="body">Email body (HTML).</param>
        Task SendEmailAsync(string to, string subject, string body);
    }
}