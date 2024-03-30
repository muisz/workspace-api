using WorkspaceAPI.Enums;

namespace WorkspaceAPI.Models
{
    public class WorkspaceMember
    {
        public int Id { get; set; }
        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public WorkspaceMemberRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
