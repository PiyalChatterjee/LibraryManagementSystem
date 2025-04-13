using LMS.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCopiesController : ControllerBase
    {
        private readonly LMSDbContext dbContext;

        public BookCopiesController(LMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Get all book copies
        // GET: api/bookcopies
        [HttpGet]
        public IActionResult GetAll()
        {
            var bookCopies = dbContext.BookCopies.ToList();
            return Ok(bookCopies);
        }
    }
}
