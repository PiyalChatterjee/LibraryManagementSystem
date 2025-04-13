using LMS.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookBorrowingsController : ControllerBase
    {
        private readonly LMSDbContext dbContext;

        public BookBorrowingsController(LMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Get all book borrowings
        // GET: api/bookborrowings
        [HttpGet]
        public IActionResult GetAll()
        {
            var bookBorrowings = dbContext.BookBorrowings.ToList();
            return Ok(bookBorrowings);
        }
    }
}
