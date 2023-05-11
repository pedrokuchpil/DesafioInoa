using System.Net;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Broker
{
    public class Email
    {
        public string status { get; set; } = "NONSENT";
        public string apiKey { get; set; }
        public SendGridClient client { get; set; }
        public Email (string apiKey)
        {
            this.apiKey = apiKey;
            this.client = new SendGridClient(apiKey);
        }
        public async Task sendEmail(string receiverEmail, string emailSubject, string emailContent, string htmlContent)
        {
            var senderEmail = new EmailAddress("pedrokuchpil@gmail.com", "Broker Pedro");
            var receiver = new EmailAddress(receiverEmail);
            var msg = MailHelper.CreateSingleEmail(senderEmail, receiver, emailSubject, emailContent, htmlContent);
            var resp = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}    