using System.ComponentModel.DataAnnotations;

namespace WorkspaceAPI.Data
{
    public class AuthRegisterData
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
    }
}
