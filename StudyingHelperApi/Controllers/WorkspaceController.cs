using Microsoft.AspNetCore.Mvc;
using StudyingHelperApi.Models;
using System.Text.Json;

namespace StudyingHelperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspaceController : Controller
    {
        private readonly StudyHelperContext dataBase;
        public WorkspaceController(StudyHelperContext context)
        {
            dataBase = context;
        }

        [HttpPost]
        [Route("add")]
        public JsonResult AddWorkspace(string username, Workspace workspace)
        {
            var user = dataBase.users.FirstOrDefault(u => u.Username == username);
            if (user == null) { return Json("Unknown error :("); }
            user.Workspaces.Add(workspace);
            dataBase.SaveChanges();
            var lastWorkspace = user.Workspaces.LastOrDefault( w=> w.Name == workspace.Name);
            return Json(workspace);
        }

        [HttpPost]
        [Route("delete")]
        public void DeleteWorkspace(Workspace workspace)
        {
            var w = dataBase.workspaces.FirstOrDefault(w => w.Id == workspace.Id);
            if (w == null) return;
            dataBase.workspaces.Remove(w);
            dataBase.SaveChanges();
        }

        [HttpPost]
        [Route("changename")]
        public void ChangeNameWorkspace(Workspace workspace)
        {
            var ws = dataBase.workspaces.FirstOrDefault(w=>w.Id == workspace.Id);
            if (ws == null) { return; }
            if(ws.Name == workspace.Name) { return; }
            ws.Name = workspace.Name;
            dataBase.SaveChanges();
        }

        [HttpPost]
        [Route("addtask")]
        public JsonResult AddTaskToWorkspace(Workspace workspace)
        {
            var ws = dataBase.workspaces.FirstOrDefault(w => w.Id == workspace.Id);
            if (ws == null) { return new JsonResult("WorkspaceNotFound"); }
            ws.Tasks = workspace.Tasks;
            dataBase.SaveChanges();
            return Json(ws);
        }
    }
}
