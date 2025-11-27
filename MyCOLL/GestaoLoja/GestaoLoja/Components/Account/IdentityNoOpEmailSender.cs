using GestaoLoja.Data;
using Microsoft.AspNetCore.Identity;

namespace GestaoLoja.Components.Account
{
    // Este é o nosso serviço de email "falso" (No-Op).
    // Ele cumpre o contrato do IEmailSender, mas não faz nada.
    internal sealed class IdentityNoOpEmailSender : IEmailSender<ApplicationUser>
    {
        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) =>
            Task.CompletedTask;

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
            Task.CompletedTask;

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
            Task.CompletedTask;
    }
}