using System.Net.Mail;

namespace WorkspaceAPI.Services
{
    public interface IEmailClient
    {
        public void Send(MailMessage message);
        public string GetSenderEmail();
    }
}