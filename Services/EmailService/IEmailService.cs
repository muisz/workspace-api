using WorkspaceAPI.Models;

namespace WorkspaceAPI.Services
{
    public interface IEmailService
    {
        public void SendOTPRegistration(OTP otp, User user);
    }
}
