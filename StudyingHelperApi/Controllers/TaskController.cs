using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudyingHelperApi.DataTransferObjects;
using StudyingHelperApi.Models;
using Task = StudyingHelperApi.Models.Task;

namespace StudyingHelperApi.Controllers
{
    [Route("api/user/{userId}")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly StudyHelperContext dataBase;
        private readonly IMapper mapper;
        public TaskController(StudyHelperContext database, IMapper mapper) 
        { 
            this.dataBase = database;
            this.mapper = mapper;
        }

        [HttpPost("workspace/{workspaceId}/task/state")]
        public IActionResult ChangeState(int userId, int workspaceId, Task task)
        {
            var t = dataBase.tasks.FirstOrDefault(t => t.Id == task.Id);
            if (t == null) return NotFound();
            t.State = task.State;
            dataBase.SaveChanges();
            return Ok();
        }

        [HttpDelete("workspace/{workspaceId}/task/{id}")]
        public IActionResult DeleteTask(int userId, int workspaceId, int id)
        {
            var task = dataBase.tasks.FirstOrDefault(t=>t.Id == id);
            if(task == null) 
                return NotFound();
            var user = dataBase.users.FirstOrDefault(u => u.Id == userId);
            if(user == null) 
                return NotFound();
            var workspace = user.Workspaces.FirstOrDefault(u => u.Id == workspaceId);
            if(workspace == null)
                return NotFound();
            workspace.Tasks.Remove(task);
            dataBase.SaveChanges();
            return Ok();
        }

        [HttpPost("workspace/{workspaceId}/task/{id}/deadline")]
        public IActionResult SetTaskDeadline(int userId, int workspaceId, int id, object date)
        {
            var dateTime = DateOnly.Parse(date.ToString());
            var task = dataBase.tasks.FirstOrDefault(t=>t.Id ==id);
            if(task == null) return BadRequest();
            task.Deadline = dateTime;
            dataBase.SaveChanges();
            return Ok(task);
        }

        [HttpGet("task")]
        public IActionResult GetUserTasks(int userId, int workspaceId)
        {
            var user = dataBase.users.FirstOrDefault(u=> u.Id == userId);
            if (user == null) return BadRequest();
            var response = user.Workspaces.SelectMany(w => w.Tasks).ToList();
            return Ok(response);
        }

        [HttpPost("workspace/{workspaceId}/task")]
        public IActionResult AddTaskToWorkspace(int userId, int workspaceId, [FromBody]TaskToCreateDto task)
        {
            if(task == null) 
                return BadRequest("Empty object sent!");
            var ws = dataBase.workspaces.FirstOrDefault(w => w.Id == workspaceId);
            if (ws == null)
                return NotFound();
            var taskEntity = mapper.Map<Task>(task);
            ws.Tasks.Add(taskEntity);
            dataBase.SaveChanges();
            return Ok(ws);
        }
    }
}
