using System.ComponentModel.DataAnnotations;

namespace WorkspaceAPI.Data
{
    public class EmailOTPData
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Code { get; set; } = string.Empty;
    }
}
