using System.Net;
using System.Net.Mail;

namespace ContactsApp.Helpers {
    public class Email : IEmail {
        private readonly IConfiguration _configuration;
        public Email(IConfiguration configuration) {
            _configuration=configuration;
        }

        public bool Send(string email, string subject, string message) {
            try {
                string host = _configuration.GetValue<string>("SMTP:Host");
                string name = _configuration.GetValue<string>("SMTP:Name");
                string username = _configuration.GetValue<string>("SMTP:Username");
                string password = _configuration.GetValue<string>("SMTP:Password");
                int port = _configuration.GetValue<int>("SMTP:Port");

                MailMessage mail = new MailMessage() {
                    From = new MailAddress(username, name),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml= true,
                    Priority = MailPriority.High
                };

                mail.To.Add(email);

                using (SmtpClient smtp = new SmtpClient(host, port)) {
                    smtp.Credentials = new NetworkCredential(username, password);
                    smtp.EnableSsl= true;
                    smtp.Send(mail);
                    return true;
                }
                
            }
            catch (Exception e) {
                return false;
            }
        }
    }
}
