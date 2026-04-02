
    using Common.Interfaces;
    using Common.Models;

    namespace Server.Services
    {
        // Explicitly implement the IEmailSender<TUser> from Common.Interfaces to avoid
        // ambiguity with other IEmailSender types (for example the one in
        // Microsoft.AspNetCore.Identity.UI.Services).
        public class IdentityEmailSender : Common.Interfaces.IEmailSender<Common.Models.ApplicationUser>
        {
            private readonly IEmailService emailService;

            public IdentityEmailSender(IEmailService emailService)
            {
                this.emailService = emailService;
            }

            public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
            {
                var subject = "Confirm your email";
                var body = $"Please confirm your account by clicking here: <a href='{confirmationLink}'>Confirm Email</a>";

                await emailService.SendEmailAsync(email, subject, body);
            }

            public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
            {
                var subject = "Reset your password";
                var body = $"Reset your password by clicking here: <a href='{resetLink}'>Reset Password</a>";

                await emailService.SendEmailAsync(email, subject, body);
            }

            public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
            {
                var subject = "Reset your password";
                var body = $"Your reset code is: {resetCode}";

                await emailService.SendEmailAsync(email, subject, body);
            }
        }
    }
