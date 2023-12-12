namespace Noteing.API.Services
{
    public class MailService
    {
        // TODO email injection
        public Task SendPasswordResetEmail(string email, string firstname, string token)
        {
            return Task.CompletedTask;
        }
    }
}
