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
            var workspaces = user.Workspaces.ToList();
            var workspaceEntity = mapper.Map<Workspace>(workspace);
            workspaces.Add(workspaceEntity);
            user.Workspaces = workspaces;
            dataBase.SaveChanges();
            return Ok(workspaceEntity);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWorkspace(int userId, int id)
        {
            var w = dataBase.workspaces.FirstOrDefault(w => w.Id == id);
            if (w == null)
                return NotFound();
            dataBase.workspaces.Remove(w);
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
        public IActionResult GetUserWorkspacec(int userId)
        {
            var u = dataBase.users.FirstOrDefault(u => u.Id == userId);
            if (u == null) return NotFound();
            return Ok(u.Workspaces);
        }
    }
}
