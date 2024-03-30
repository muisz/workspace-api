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
        public ActionResult<PostWorkspaceResponse> PostCreateWorkspace(CreateWorkspace payload)
        {
            try
            {
                User? user = _authService.GetUserByEmail(User?.Claims.First(c => c.Type == ClaimTypes.Email).Value ?? "");
                if (user == null)
                    throw new Exception("User not found");
                
                Workspace workspace = _workspaceService.CreateWorkspace(payload, user);
                return Ok(new PostWorkspaceResponse { Id = workspace.Id });
            }
            catch (Exception error)
            {
                return Problem(error.Message, statusCode: StatusCodes.Status400BadRequest);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult<ICollection<WorkspaceResponse>> GetWorkspaces()
        {
            try
            {
                User? user = _authService.GetUserByEmail(User?.Claims.First(c => c.Type == ClaimTypes.Email).Value ?? "");
                if (user == null)
                    throw new Exception("User not found");
                
                ICollection<Workspace> workspaces = _workspaceService.GetWorkspaces(user);
                ICollection<WorkspaceResponse> responses = new List<WorkspaceResponse>();
                foreach (Workspace workspace in workspaces)
                {
                    WorkspaceResponse workspaceResponse = new WorkspaceResponse{
                        Id = workspace.Id,
                        Name = workspace.Name,
                        CreatedAt = workspace.CreatedAt,
                    };
                    foreach (WorkspaceMember member in workspace.Members)
                    {
                        workspaceResponse.Members.Add(new WorkspaceMemberResponse{
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
    }
}