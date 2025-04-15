using AutoMapper;
using LMS.API.Data;
using LMS.API.Models.DTOs;
using LMS.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LMSDbContext dbContext;
        private readonly IBookRepository bookRepository;
        private readonly IMapper mapper;

        public BooksController(LMSDbContext dbContext, IBookRepository bookRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.bookRepository = bookRepository;
            this.mapper = mapper;
        }
        //Get all books
        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] BookQueryParamsDTO bookQueryParams)
        {
            var bookEntities = await bookRepository.GetAllBooksDetailsAsync(bookQueryParams.Status, bookQueryParams.SortBy, bookQueryParams.IsAscending);
            // Data mapping section
            // Return the list of books with a 200 OK response
            return Ok(mapper.Map<List<BookDetailsDTO>>(bookEntities));
        }
        //Get book by id
        // GET: api/books/{id}
        [HttpGet("{id:Guid}", Name = "GetBookById")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var bookEntity = await bookRepository.GetBookDetailsByIdAsync(id);
            if (bookEntity == null)
            {
                return NotFound();
            }
            // Data mapping section
            // Return the book with a 200 OK response
            return Ok(mapper.Map<BookDetailsDTO>(bookEntity));
        }
    }
}
