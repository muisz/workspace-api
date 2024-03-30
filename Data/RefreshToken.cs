using System.ComponentModel.DataAnnotations;

namespace WorkspaceAPI.Data
{
    public class RefreshToken
    {
        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
