using System.Net.Mail;
using System.Net;

namespace GestaoPortifolioInvestimento.Application.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }

    public class EmailNotificacaoService : IEmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailNotificacaoService()
        {
            _smtpClient = new SmtpClient("smtp.example.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("username", "password"),
                EnableSsl = true,
            };
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("noreply@example.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(to);

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
