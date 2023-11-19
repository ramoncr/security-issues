namespace Noteing.API.Services
{
    public class MailService
    {
        public Task SendPasswordResetEmail(string email, string token)
        {
            return Task.CompletedTask;
        }
    }
}
