using System.Net.Mail;
using System.Net;

namespace Logger
{
    internal class Sender
    {
        private string senderEmail = "yourEmail";
        private string senderPassword = "yourPassword";
        private string recipientEmail = "yourEmail";

        public void Send(string text)
        {
            MailAddress from = new MailAddress(senderEmail, "test");
            MailAddress to = new MailAddress(recipientEmail);

            MailMessage message = new MailMessage(from, to);

            message.Subject = "Record";
            message.Body = text;
            message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtp.EnableSsl = true;

            smtp.Send(message);
        }
    }
}