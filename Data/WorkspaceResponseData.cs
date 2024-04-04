namespace WorkspaceAPI.Data
{
    public class WorkspaceResponseData
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public ICollection<WorkspaceMemberResponseData> Members { get; set; } = new List<WorkspaceMemberResponseData>();
    }
}