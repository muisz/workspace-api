namespace WorkspaceAPI.Data
{
    public class PostLoginResponseData
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public DateTime? LastActive { get; set; }
        public TokenData? Token { get; set; }
    }
}
