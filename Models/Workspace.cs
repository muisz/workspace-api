namespace WorkspaceAPI.Models
{
    public class Workspace
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public int CreatorId { get; set; }
        public User Creator { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public ICollection<WorkspaceMember> Members { get; } = new List<WorkspaceMember>();
    }
}
