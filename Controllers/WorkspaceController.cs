using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkspaceAPI.Data;
using WorkspaceAPI.Models;
using WorkspaceAPI.Services;

namespace WorkspaceAPI.Controllers
{
    [Route("/api/v1/workspaces")]
    [ApiController]
    public class WorkspaceController : ControllerBase
    {
        private readonly IWorkspaceService _workspaceService;
        private readonly IAuthService _authService;

        public WorkspaceController(IWorkspaceService workspaceService, IAuthService authService)
        {
            _workspaceService = workspaceService;
            _authService = authService;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<PostWorkspaceResponseData> PostCreateWorkspace(CreateWorkspaceData payload)
        {
            try
            {
                User? user = _authService.GetUserByEmail(User?.Claims.First(c => c.Type == ClaimTypes.Email).Value ?? "");
                if (user == null)
                    throw new Exception("User not found");
                
                Workspace workspace = _workspaceService.CreateWorkspace(payload, user);
                return Ok(new PostWorkspaceResponseData { Id = workspace.Id });
            }
            catch (Exception error)
            {
                return Problem(error.Message, statusCode: StatusCodes.Status400BadRequest);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult<ICollection<WorkspaceResponseData>> GetWorkspaces()
        {
            try
            {
                User? user = _authService.GetUserByEmail(User?.Claims.First(c => c.Type == ClaimTypes.Email).Value ?? "");
                if (user == null)
                    throw new Exception("User not found");
                
                ICollection<Workspace> workspaces = _workspaceService.GetWorkspaces(user);
                ICollection<WorkspaceResponseData> responses = new List<WorkspaceResponseData>();
                foreach (Workspace workspace in workspaces)
                {
                    WorkspaceResponseData workspaceResponse = new WorkspaceResponseData{
                        Id = workspace.Id,
                        Name = workspace.Name,
                        CreatedAt = workspace.CreatedAt,
                    };
                    foreach (WorkspaceMember member in workspace.Members)
                    {
                        workspaceResponse.Members.Add(new WorkspaceMemberResponseData{
                            Id = member.Id,
                            UserId = member.UserId,
                            Name = member.User.Name,
                            Role = member.Role,
                            CreatedAt = member.CreatedAt,
                        });
                    }
                    responses.Add(workspaceResponse);
                }
                return Ok(responses);
            }
            catch (Exception error)
            {
                return Problem(error.Message, statusCode: StatusCodes.Status400BadRequest);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<WorkspaceResponseData> GetWorkspace(int id)
        {
            try
            {
                User? user = _authService.GetUserByEmail(User?.Claims.First(c => c.Type == ClaimTypes.Email).Value ?? "");
                if (user == null)
                    throw new Exception("User not found");
                
                Workspace? workspace = _workspaceService.GetWorkspace(id);
                if (workspace == null || !_workspaceService.IsMemberOfWorkspace(workspace, user))
                    throw new Exception("Workspace not found");
                
                WorkspaceResponseData response = new WorkspaceResponseData
                {
                    Id = workspace.Id,
                    Name = workspace.Name,
                    CreatedAt = workspace.CreatedAt,
                };
                foreach (WorkspaceMember member in workspace.Members)
                {
                    response.Members.Add(new WorkspaceMemberResponseData {
                        Id = member.Id,
                        UserId = member.UserId,
                        Name = member.User.Name,
                        Role = member.Role,
                        CreatedAt = member.CreatedAt,
                    });
                }
                return Ok(response);

            }
            catch (Exception error)
            {
                return Problem(error.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }
    }
}