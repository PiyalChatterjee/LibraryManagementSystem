using LMS.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly LMSDbContext dbContext;

        public MembersController(LMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Get all members
        // GET: api/members
        [HttpGet]
        public IActionResult GetAll()
        {
            var members = dbContext.Members.ToList();
            return Ok(members);
        }
    }
}
