using LMS.API.Data;
using LMS.API.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LMSDbContext dbContext;

        public UsersController(LMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Get all users
        // GET: api/users
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = dbContext.Users.ToList();
            return Ok(users);
        }
    }
}
