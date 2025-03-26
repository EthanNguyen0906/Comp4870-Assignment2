using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Assignment2Server.Components.Account
{
    public class IdentityNoOpEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
