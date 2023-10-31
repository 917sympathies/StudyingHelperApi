using Microsoft.AspNetCore.Mvc;
using StudyingHelperApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace StudyingHelperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        public static User User { get; private set; }

        private StudyHelperContext database;

        public UserController(StudyHelperContext database) { this.database = database; }

        [HttpPost]
        [Route("signin")]
        public JsonResult AuthorizeUser(User user )
        {
            var data = database.users.ToList();
            var passHash = PasswordHandler.GetPasswordHash(user.Password);
            var result = database.users.FirstOrDefault(u => u.Username == user.Username && u.Password == passHash);
            if(result  == null) return Json(new { Error = "NotFound"});
            User = result;
            return Json(result);
        }


        [HttpPost]
        [Route("signup")]
        public JsonResult RegistrateUser(User user)
        {
            user.Password = PasswordHandler.GetPasswordHash(user.Password);
            database.users.Add(user);
            database.SaveChanges();
            User = database.users.FirstOrDefault(u => u.Username == user.Username);
            return Json(User);
        }

        [HttpGet]
        [Route("getWorkspaces")]
        public JsonResult GetUserWorkspacec(string username)
        {
            var u = database.users.FirstOrDefault(u=>u.Username == username);
            if (u == null) return Json(new { Error = "NotFound" });
            return Json(u.Workspaces);
        }
    }

    public static class PasswordHandler
    {
        public static string GetPasswordHash(string password)
        {
            var md5 = MD5.Create();
            return Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
