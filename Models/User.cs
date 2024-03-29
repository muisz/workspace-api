namespace WorkspaceAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name  { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool EmailVerified { get; set; } = false;
        public string Password { get; set; } = string.Empty;
        public DateTime? LastActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
