using System.ComponentModel.DataAnnotations;

namespace WorkspaceAPI.Data
{
    public class CreateWorkspaceData
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
