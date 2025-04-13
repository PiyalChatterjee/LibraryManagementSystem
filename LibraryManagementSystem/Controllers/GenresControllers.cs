using LMS.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresControllers : ControllerBase
    {
        private readonly LMSDbContext dbContext;

        public GenresControllers(LMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Get all genres
        // GET: api/genres
        [HttpGet]
        public IActionResult GetAll()
        {
            var genres = dbContext.Genres.ToList();
            return Ok(genres);
        }
    }
}
