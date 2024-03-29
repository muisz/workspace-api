namespace WorkspaceAPI.Data
{
    public class PostLoginResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public DateTime? LastActive { get; set; }
    }
}
