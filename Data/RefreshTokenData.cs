using System.ComponentModel.DataAnnotations;

namespace WorkspaceAPI.Data
{
    public class RefreshTokenData
    {
        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
