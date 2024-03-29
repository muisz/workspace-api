using WorkspaceAPI.Enums;
using WorkspaceAPI.Models;

namespace WorkspaceAPI.Services
{
    public interface IOTPService
    {
        public OTP CreateOTP(OTPUsage usage, string destination);
        public void DeactivatePreviousOTP(OTPUsage usage, string destination);
        public OTP? GetOTP(OTPUsage usage, string destination, string code);
        public bool IsValid(OTP otp);
        public bool IsExpired(OTP otp);
        public void Deactivate(OTP otp);
    }
}