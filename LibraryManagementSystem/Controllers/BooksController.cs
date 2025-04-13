using LMS.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LMSDbContext dbContext;

        public BooksController(LMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Get all books
        // GET: api/books
        [HttpGet]
        public IActionResult GetAll()
        {
            var books = dbContext.Books.ToList();
            return Ok(books);
        }
    }
}
