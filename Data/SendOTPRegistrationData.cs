using System.ComponentModel.DataAnnotations;

namespace WorkspaceAPI.Data
{
    public class SendOTPRegistrationData
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
