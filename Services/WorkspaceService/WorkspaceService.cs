using Microsoft.EntityFrameworkCore;
using WorkspaceAPI.Data;
using WorkspaceAPI.Models;

namespace WorkspaceAPI.Services
{
    public class WorkspaceService : IWorkspaceService
    {
        private readonly DatabaseContext _context;

        public WorkspaceService(DatabaseContext context)
        {
            _context = context;
        }

        public Workspace CreateWorkspace(CreateWorkspace payload, User user)
        {
            Workspace workspace = new Workspace
            {
                Name = payload.Name,
                Description = payload.Description,
                CreatorId = user.Id,
                CreatedAt = DateTime.Now.ToUniversalTime(),
            };
            WorkspaceMember member = new WorkspaceMember
            {
                UserId = user.Id,
                Role = Enums.WorkspaceMemberRole.Organizer,
                CreatedAt = DateTime.Now.ToUniversalTime(),
            };

            workspace.Members.Add(member);
            user.WorkspaceCreators.Add(workspace);
            _context.SaveChanges();
            
            return workspace;
        }

        public ICollection<Workspace> GetWorkspaces(User user)
        {
            return _context.Workspaces.Include(workspace => workspace.Members.Where(member => member.UserId == user.Id)).ToList();
        }

        public Workspace? GetWorkspace(int id)
        {
            return _context.Workspaces.SingleOrDefault(workspace => workspace.Id == id);
        }

        public bool IsMemberOfWorkspace(Workspace workspace, User user)
        {
            bool isMember = false;
            foreach (WorkspaceMember member in workspace.Members)
            {
                if (member.UserId == user.Id)
                {
                    isMember = true;
                    break;
                }
            }
            return isMember;
        }
    }
}