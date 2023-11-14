using Assignment3_Torres_JoseDavid.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Assignment3_Torres_JoseDavid.Services
{
    /*
    * Course:      Web Programming 3
    * Assessment:  Assignment 3
    * Created by:  Jose David Torres
    * Date:        14/11/2023
    * Class Name:  SendGridEmailSender.cs
    * Description: Class that contains the methods required to send emails to the user.
    */
    public class SendGridEmailSender : IEmailSender
    {
        public ContactModel Contact { get; set; }

        public SendGridEmailSender(IOptions<SendGridEmailSenderOptions> options)
        {
            this.Options = options.Value;
        }

        public SendGridEmailSenderOptions Options { get; set; }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await Execute(Options.ApiKey, subject, message, email);
        }

        private async Task<Response> Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var senderEmail = new EmailAddress(Options.SenderEmail, Options.SenderName);
            var customerEmail = new EmailAddress(email);
            var msg = new SendGridMessage()
            {
                From = senderEmail,
                Subject = subject,
                PlainTextContent =  "Topic: " + subject + ". Thanks for your message, " + Contact.FirstName + " " + Contact.LastName + "." +
                                    "We received the following information: Postal Code: " + Contact.PostalCode + ", Email: " + email + "," +
                                    "Phone Number: " + Contact.Phone + ". Your message: " + message,
                HtmlContent =   "<h1>Topic: " + subject + "</h1><br/>" +
                                "<p>Thanks for your message, " + Contact.FirstName + " " + Contact.LastName + "</p><br/>" +
                                "<p>We received the following information:</p><br/>" +
                                "<ul>" +
                                    "<li>Postal Code: " + Contact.PostalCode + "</li>" +
                                    "<li>Email: " + email + "</li>" +
                                    "<li>Phone Number: " + Contact.Phone + "</li>" +
                                "</ul><br/>" +
                                "<h2>Your message:</h2><br/>" + message,
                ReplyTo = customerEmail,
            };
            msg.AddTo(customerEmail);
            msg.AddTo(new EmailAddress(Options.SenderEmail));

            msg.SetClickTracking(false, false);
            msg.SetOpenTracking(false);
            msg.SetGoogleAnalytics(false);
            msg.SetSubscriptionTracking(false);

            return await client.SendEmailAsync(msg);

        }
    }
}
