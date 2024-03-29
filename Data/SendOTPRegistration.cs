using System.ComponentModel.DataAnnotations;

namespace WorkspaceAPI.Data
{
    public class SendOTPRegistration
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
