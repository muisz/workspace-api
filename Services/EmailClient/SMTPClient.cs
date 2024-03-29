using System.Net;
using System.Net.Mail;

namespace WorkspaceAPI.Services
{
    public class SMTPClient : IEmailClient
    {
        private readonly SmtpClient _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public SMTPClient(IConfiguration configuration, ILogger<SMTPClient> logger)
        {
            _configuration = configuration;
            _logger = logger;

            string? host = _configuration["SMTP:Host"];
            string? user = _configuration["SMTP:User"];
            string? password = _configuration["SMTP:Password"];

            if (!int.TryParse(_configuration["SMTP:Port"], out int port))
                port = 587;


            _client = new SmtpClient(host)
            {
                Port = port,
                Credentials = new NetworkCredential(user, password),
                EnableSsl = true,
            };
        }

        public void Send(MailMessage message)
        {
            try
            {
                _client.Send(message);
            }
            catch (Exception error)
            {
                _logger.LogError($"SMTP Error: {error.Message}");
            }
        }

        public string GetSenderEmail()
        {
            return _configuration["SMTP:Sender"] ?? "";
        }
    }
}