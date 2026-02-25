// Copyright (c) 2025. All rights reserved.

namespace Common.Interfaces
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
        /// <returns>A task that represents the asynchronous send operation.</returns>
        Task SendEmailAsync(string to, string subject, string body);
    }
}
