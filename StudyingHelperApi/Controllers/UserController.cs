using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudyingHelperApi.DataTransferObjects;
using StudyingHelperApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace StudyingHelperApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly StudyHelperContext database;
        private readonly IMapper mapper;

        public UserController(StudyHelperContext database, IMapper mapper) 
        { 
            this.database = database;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult AuthorizeUser([FromBody]UserToSignInDto user)
        {
            if (user == null) 
                return BadRequest("Empty object was sent!");
            var passHash = PasswordHandler.GetPasswordHash(user.Password);
            var result = database.users.FirstOrDefault(u => u.Username == user.Username && u.Password == passHash);
            if(result  == null) 
                return NotFound();
            var userToReturn = mapper.Map<UserDto>(result);
            return Ok(userToReturn);
        }


        [HttpPost]
        [Route("signup")]
        public IActionResult RegistrateUser([FromBody]UserToCreationDto user)
        {
            if (user == null)
                return BadRequest("Empty object was sent!");
            var userEntity = mapper.Map<User>(user);
            userEntity.Password = PasswordHandler.GetPasswordHash(user.Password);
            database.users.Add(userEntity);
            database.SaveChanges();
            var userToReturn = mapper.Map<UserDto>(userEntity);
            return Ok(userToReturn);
        }
    }
}
