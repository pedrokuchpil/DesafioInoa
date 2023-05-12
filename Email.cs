using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Broker
{
    public class Email
    {
        public string status { get; set; } = "NONSENT";

        public SmtpClient smtpClient { get; set; }
        public Email (string configfile)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(configfile).Build();
            this.smtpClient = new SmtpClient(config["Smtp:Host"])
            {
                Port = int.Parse(config["Smtp:Port"]!),
                Credentials = new NetworkCredential(config["Smtp:Username"], config["Smtp:Password"]),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
        }
        public void sendEmail(string receiverEmail, string emailSubject, string emailContent)
        {
            smtpClient.Send("broker@pk.com", receiverEmail, emailSubject, emailContent);
        }
    }
}    