using WorkspaceAPI.Data;
using WorkspaceAPI.Models;

namespace WorkspaceAPI.Services
{
    public interface IWorkspaceService
    {
        public Workspace CreateWorkspace(CreateWorkspace payload, User user);
        public ICollection<Workspace> GetWorkspaces(User user);
    }
}