using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudyingHelperApi.Models;
using Task = StudyingHelperApi.Models.Task;

namespace StudyingHelperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private StudyHelperContext database;
        public TaskController(StudyHelperContext database) { this.database = database; }

        [HttpPost]
        [Route("changeState")]
        public IActionResult changeState(Task task)
        {
            var t = database.tasks.FirstOrDefault(t => t.Id == task.Id);
            if (t == null) return NotFound();
            t.State = task.State;
            database.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("delete/{id}")]
        public IActionResult deleteTask(int id)
        {
            var task = database.tasks.FirstOrDefault(t=>t.Id == id);
            if(task == null) return NotFound();
            database.tasks.Remove(task);
            database.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("setDeadline/{id}")]
        public JsonResult SetTaskDeadline(int id, object date)
        {
            var dateTime = DateOnly.Parse(date.ToString());
            var task = database.tasks.FirstOrDefault(t=>t.Id ==id);
            if(task == null) return new JsonResult("Error");
            task.Deadline = dateTime;
            database.SaveChanges();
            return Json(task);
        }

        [HttpGet]
        [Route("getTasks/{id}")]
        public JsonResult GetUserTasks(int id)
        {
            var user = database.users.FirstOrDefault(u=> u.Id == id);
            if (user == null) return Json("Error");
            var response = user.Workspaces.SelectMany(w => w.Tasks).ToList();
            return Json(response);
        } 
    }
}
