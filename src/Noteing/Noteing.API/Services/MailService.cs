using MimeKit;
using Noteing.API.Models;
using MailKit.Net.Smtp;

namespace Noteing.API.Services
{
    public class MailService
    {
        private readonly EmailConfiguration _emailConfig;

        public MailService(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task SendPasswordResetEmail(string email, string firstname, string token)
        {
            var message = new Message
            {
                Content = $"Hi {firstname}, <br /> Here is the link to reset your password. Please to to <a href=\"http://noteing.local/reset/{token}\">http://noteing.local/reset/{token}</a>",
                Subject = "Password reset",
                To = new MailboxAddress(email),
            };


            var emailMessage = CreateEmailMessage(message);
            await SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.Add(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };

            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
