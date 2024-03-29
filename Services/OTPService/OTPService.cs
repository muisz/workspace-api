using WorkspaceAPI.Enums;
using WorkspaceAPI.Models;

namespace WorkspaceAPI.Services
{
    public class OTPService : IOTPService
    {
        private readonly DatabaseContext _context;

        public OTPService(DatabaseContext context)
        {
            _context = context;
        }

        public OTP CreateOTP(OTPUsage usage, string destination)
        {
            DeactivatePreviousOTP(usage, destination);

            Random random = new Random();
            string code = random.Next(1000, 9999).ToString();
            OTP otp = new OTP
            {
                Usage = usage,
                Destination = destination,
                Code = code,
                IsActive = true,
                ValidUntil = DateTime.Now.AddMinutes(10).ToUniversalTime(),
                CreatedAt = DateTime.Now.ToUniversalTime(),
            };

            _context.OTPs.Add(otp);
            _context.SaveChanges();
            return otp;
        }

        public void DeactivatePreviousOTP(OTPUsage usage, string destination)
        {
            List<OTP> otps = _context.OTPs.Where(otp => 
                otp.Usage == usage && otp.Destination.Equals(destination)).ToList();
            
            otps.ForEach(otp => {
                otp.IsActive = false;
            });
            _context.SaveChanges();
        }

        public OTP? GetOTP(OTPUsage usage, string destination, string code)
        {
            return _context.OTPs.SingleOrDefault(otp => otp.Code.Equals(code) && otp.Destination.Equals(destination));
        }

        public bool IsValid(OTP otp)
        {
            if (!otp.IsActive)
                throw new Exception("OTP already used or not active");
            
            if (IsExpired(otp))
                throw new Exception("OTP expired");
            
            return true;
        }

        public bool IsExpired(OTP otp)
        {
            return DateTime.Now.ToUniversalTime().CompareTo(otp.ValidUntil) > 0;
        }

        public void Deactivate(OTP otp)
        {
            otp.IsActive = false;
            _context.SaveChanges();
        }
    }
}
