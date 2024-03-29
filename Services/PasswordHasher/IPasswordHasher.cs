namespace WorkspaceAPI.Services
{
    public interface IPasswordHasher
    {
        public string Hash(string password);
        public bool Check(string password, string hash);
    }
}
