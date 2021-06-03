using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.PL.Util
{
    public class EmailService : IEmailSender
    {
        public EmailService(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager
        public Task SendEmailAsync(string email, string subject, string message)
        {
            Task<string> result;
            return result = Execute(Options.SendGridKey, subject, message, email);
        }

        public async Task<string> Execute(string apiKey, string subject, string message, string email)
        {
            //var client = new SendGridClient(apiKey);
            //var msg = new SendGridMessage()
            //{
            //    From = new EmailAddress("fariborzsaidi@hotmail.com", Options.SendGridUser),
            //    Subject = subject,
            //    PlainTextContent = message,
            //    HtmlContent = message
            //};
            //msg.AddTo(new EmailAddress(email));

            //// Disable click tracking.
            //// See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            //msg.SetClickTracking(false, false);
            //var response = await client.SendEmailAsync(msg);
            //return response.StatusCode.ToString();

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("fariborzsaidi@hotmail.com", "saidi");
            var to = new EmailAddress(email, "saidi");
            var emailframe = MailHelper.CreateSingleEmail(@from, to, subject, message, message);
            var response = await client.SendEmailAsync(emailframe);
            return response.StatusCode.ToString();
        }
    }
}
