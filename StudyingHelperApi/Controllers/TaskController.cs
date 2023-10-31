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
        [Route("delete")]
        public IActionResult deleteTask(Task task)
        {
            var t = database.tasks.FirstOrDefault(t=>t.Id == task.Id);
            if(t == null) return NotFound();
            database.tasks.Remove(t);
            database.SaveChanges();
            return Ok();
        }
    }
}
