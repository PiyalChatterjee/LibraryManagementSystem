using LMS.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly LMSDbContext dbContext;

        public AuthorController(LMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Get all authors
        // GET: api/authors
        [HttpGet]
        public IActionResult GetAll()
        {
            var authors = dbContext.Authors.ToList();
            return Ok(authors);
        }
    }
}
