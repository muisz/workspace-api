using System.ComponentModel.DataAnnotations;

namespace WorkspaceAPI.Data
{
    public class EmailOTP
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Code { get; set; } = string.Empty;
    }
}
