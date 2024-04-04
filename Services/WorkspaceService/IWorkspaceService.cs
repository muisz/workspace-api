using WorkspaceAPI.Data;
using WorkspaceAPI.Models;

namespace WorkspaceAPI.Services
{
    public interface IWorkspaceService
    {
        public Workspace CreateWorkspace(CreateWorkspaceData payload, User user);
        public ICollection<Workspace> GetWorkspaces(User user);
        public Workspace? GetWorkspace(int id);
        public bool IsMemberOfWorkspace(Workspace workspace, User user);
    }
}