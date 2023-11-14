using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudyingHelperApi.DataTransferObjects;
using StudyingHelperApi.Models;
using System.Text.Json;

namespace StudyingHelperApi.Controllers
{
    [Route("api/user/{userId}/workspace")]
    [ApiController]
    public class WorkspaceController : Controller
    {
        private readonly StudyHelperContext dataBase;
        private readonly IMapper mapper;
        public WorkspaceController(StudyHelperContext dataBase, IMapper mapper)
        {
            this.dataBase = dataBase;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddWorkspace(int userId, [FromBody] WorkspaceToCreationDto workspace)
        {
            if (workspace == null) 
                return BadRequest("Empty object was sent!");
            var user = dataBase.users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return NotFound();
            var workspaceEntity = mapper.Map<Workspace>(workspace);
            user.Workspaces.Add(workspaceEntity);
            dataBase.SaveChanges();
            return Ok(workspaceEntity);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWorkspace(int userId, int id)
        {
            var user = dataBase.users.FirstOrDefault(u => u.Id.Equals(userId));
            if (user == null)
                return NotFound();
            var workspace = dataBase.workspaces.FirstOrDefault(w => w.Id == id);
            if (workspace == null)
                return NotFound();
            user.Workspaces.Remove(workspace);
            dataBase.SaveChanges();
            return Ok();
        }

        [HttpPost("{id}/name")]
        public IActionResult ChangeNameWorkspace(int userId, int id, string name)
        {
            var ws = dataBase.workspaces.FirstOrDefault(w => w.Id == id);
            if (ws == null)
                return NotFound();
            if (ws.Name == name)
                return Ok();
            ws.Name = name;
            dataBase.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public IActionResult GetUserWorkspaces(int userId)
        {
            var u = dataBase.users.FirstOrDefault(u => u.Id == userId);
            if (u == null) return NotFound();
            return Ok(u.Workspaces);
        }
    }
}
