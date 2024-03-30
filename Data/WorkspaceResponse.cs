namespace WorkspaceAPI.Data
{
    public class WorkspaceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public ICollection<WorkspaceMemberResponse> Members { get; set; } = new List<WorkspaceMemberResponse>();
    }
}