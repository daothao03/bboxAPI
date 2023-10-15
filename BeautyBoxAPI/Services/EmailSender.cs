using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using System.Diagnostics;
using System.Net.Mail;

namespace BeautyBoxAPI.Services
{
    public class EmailSender
    {
        public static void SendEmail(string senderEmail, string senderName,
            string receiverEmail, string receiverName, string subject, string message)
        {
            var apiInstance = new TransactionalEmailsApi();

            SendSmtpEmailSender sender = new SendSmtpEmailSender(senderName, senderEmail);

            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(receiverEmail, receiverName);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);

            string HtmlContent = null;
            string TextContent = message;

            try
            {
                var sendSmtpEmail = new SendSmtpEmail(sender, To, null, null, HtmlContent, TextContent, subject);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);

                Console.WriteLine("BBox response: " + result.ToJson());

            }
            catch (Exception e)
            {

                Console.WriteLine("We have an exception: " + e.Message);

            }
        }


        //private readonly string apiKey;
        //private readonly string fromEmail;
        //private readonly string senderName;

        //public EmailSender(IConfiguration configuration)
        //{
        //    apiKey = configuration["EmailSender:ApiKey"]!;
        //    fromEmail = configuration["EmailSender:FromEmail"]!;
        //    senderName = configuration["EmailSender:SenderName"]!;
        //}

        //public async Task SendEmail(string subject, string toEmail, string username, string message)
        //{
        //    var client = new SendGridClient(apiKey);
        //    var from = new EmailAddress(fromEmail, senderName);
        //    var to = new EmailAddress(toEmail, username);
        //    var plainTextContent = message;
        //    var htmlContent = "";
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    var response = await client.SendEmailAsync(msg);
        //}
    }
}
