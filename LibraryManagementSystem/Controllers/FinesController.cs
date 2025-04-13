using LMS.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinesController : ControllerBase
    {
        private readonly LMSDbContext dbContext;

        public FinesController(LMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Get all fines
        // GET: api/fines
        [HttpGet]
        public IActionResult GetAll()
        {
            var fines = dbContext.Fines.ToList();
            return Ok(fines);
        }
    }
}
