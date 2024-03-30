using WorkspaceAPI.Enums;

namespace WorkspaceAPI.Data
{
    public class WorkspaceMemberResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public WorkspaceMemberRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
