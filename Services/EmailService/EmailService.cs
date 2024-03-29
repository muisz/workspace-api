using System.Net.Mail;
using WorkspaceAPI.Models;

namespace WorkspaceAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailClient _client;

        public EmailService(IEmailClient client)
        {
            _client = client;
        }

        public void SendOTPRegistration(OTP otp, User user)
        {
            string expiredDate = otp.ValidUntil.ToString("dddd, dd MMMM yyyy hh:mm tt");
            string body = $"<body>Hi {user.Name}, here is your OTP code {otp.Code}, valid until {expiredDate}</body>";
            string sender = $"Workspace API <{_client.GetSenderEmail()}>";

            MailMessage message = new MailMessage(sender, user.Email)
            {
                Subject = "OTP Registration",
                Body = body,
                IsBodyHtml = true
            };

            _client.Send(message);
        }
    }
}