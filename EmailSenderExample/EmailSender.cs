using System.Net;
using System.Net.Mail;

namespace EmailSenderExample
{
    static class EmailSender
    {
        public static void Send(string to, string message)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            NetworkCredential credential = new NetworkCredential("mail_adress","password");
            smtpClient.Credentials = credential;

            MailAddress gonderen = new MailAddress("mail_adress", "Mail Example");
            MailAddress alici = new MailAddress(to);

            MailMessage mail = new MailMessage(gonderen, alici);
            mail.Subject = "Example";
            mail.Body = message;

            smtpClient.Send(mail);
        }
    }
}
