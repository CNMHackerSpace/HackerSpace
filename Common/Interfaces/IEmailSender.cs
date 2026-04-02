// Copyright (c) 2025. All rights reserved.

using System.Threading.Tasks;

namespace Common.Interfaces
{
    /// <summary>
    /// Generic email sender interface used by the application to send account-related emails.
    /// </summary>
    /// <typeparam name="TUser">The user type.</typeparam>
    public interface IEmailSender<TUser> where TUser : class
    {
        Task SendConfirmationLinkAsync(TUser user, string email, string confirmationLink);
        Task SendPasswordResetLinkAsync(TUser user, string email, string resetLink);
        Task SendPasswordResetCodeAsync(TUser user, string email, string resetCode);
    }
}
