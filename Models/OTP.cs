using WorkspaceAPI.Enums;

namespace WorkspaceAPI.Models
{
    public class OTP
    {
        public int Id { get; set; }
        public string Destination { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public OTPUsage Usage { get; set; }
        public bool IsActive { get; set; }
        public DateTime ValidUntil { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
