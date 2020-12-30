using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLewis.Services
{
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        public async Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("admin@recipelewis.com", "Recipe Lewis");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, "d-1f0e2ee3bada4a7d9dcfd8b7e2d24926", new
            {
                Sender_Name = "John Smith",
                code = message
            });
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Email failed to send");
            }
        }

        public void Test()
        {
            //test
        }
    }
}
