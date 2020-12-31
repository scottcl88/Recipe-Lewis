using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using RecipeLewis.DataExtensions;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace RecipeLewis.Services
{
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
        public string PersonalEmail { get; set; }
    }

    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(Options.SendGridKey, subject, htmlMessage, email);
        }

        public async Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("admin@recipelewis.com", "Recipe Lewis");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
            var response = await client.SendEmailAsync(msg);
            if (!response.IsSuccessStatusCode)
            {
                throw new EmailSendException($"Email failed to send to {to.Email}");
            }
        }

        public async Task SendEmailConfirmation(string code, string senderName, string toEmail)
        {
            var client = new SendGridClient(Options.SendGridKey);
            var from = new EmailAddress("admin@recipelewis.com", "Recipe Lewis");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, "d-1f0e2ee3bada4a7d9dcfd8b7e2d24926", new
            {
                Sender_Name = senderName,
                code = code
            });
            var response = await client.SendEmailAsync(msg);
            if (!response.IsSuccessStatusCode)
            {
                throw new EmailSendException($"Email failed to send to {to.Email}");
            }
            var adminTo = new EmailAddress(Options.PersonalEmail, "Recipe Lewis");
            msg = MailHelper.CreateSingleEmail(from, adminTo, "RecipeLewis-Confirm Email", "", "");
            await client.SendEmailAsync(msg);
        }
    }
}