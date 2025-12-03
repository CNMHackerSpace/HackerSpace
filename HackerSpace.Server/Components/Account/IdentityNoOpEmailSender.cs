// Copyright (c) CNM. All rights reserved.

using HackerSpace.Data;
using HackerSpace.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace HackerSpace.Components.Account
{
    /// <summary>
    /// No-op adapter that forwards identity email requests to the project's email sender implementation.
    /// Used in development to avoid sending real emails.
    /// </summary>
    internal sealed class IdentityNoOpEmailSender : IEmailSender<ApplicationUser>
    {
        private readonly IEmailSender emailSender = new NoOpEmailSender();

        /// <summary>
        /// Sends the account confirmation email for the given user.
        /// </summary>
        /// <param name="user">The application user the email is for.</param>
        /// <param name="email">The email address to send to.</param>
        /// <param name="confirmationLink">The confirmation link the user should click.</param>
        /// <returns>A task that completes when the email has been queued.</returns>
        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) =>
            emailSender.SendEmailAsync(email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

        /// <summary>
        /// Sends the password reset link email for the given user.
        /// </summary>
        /// <param name="user">The application user the email is for.</param>
        /// <param name="email">The email address to send to.</param>
        /// <param name="resetLink">The password reset link.</param>
        /// <returns>A task that completes when the email has been queued.</returns>
        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
            emailSender.SendEmailAsync(email, "Reset your password", $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");

        /// <summary>
        /// Sends the password reset code email for the given user.
        /// </summary>
        /// <param name="user">The application user the email is for.</param>
        /// <param name="email">The email address to send to.</param>
        /// <param name="resetCode">The password reset code.</param>
        /// <returns>A task that completes when the email has been queued.</returns>
        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
            emailSender.SendEmailAsync(email, "Reset your password", $"Please reset your password using the following code: {resetCode}");
    }
}
