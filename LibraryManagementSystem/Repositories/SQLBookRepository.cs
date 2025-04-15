using LMS.API.Data;
using LMS.API.Models.Domain;
using LMS.API.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Repositories
{
    public class SQLBookRepository : IBookRepository
    {
        private readonly LMSDbContext dbContext;

        public SQLBookRepository(LMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Book> CreateAsync(Book book, Author author, Genre genre)
        {
            // Generate GUID for Book
            book.BookId = Guid.NewGuid();

            // Check if the author already exists
            var existingAuthor = await dbContext.Authors
                .FirstOrDefaultAsync(a => a.FirstName == author.FirstName && a.LastName == author.LastName);

            // Check if the genre already exists
            var existingGenre = await dbContext.Genres
                .FirstOrDefaultAsync(g => g.Name == genre.Name);

            if (existingAuthor != null)
            {
                // Use the existing author's ID
                author.AuthorId = existingAuthor.AuthorId;
            }
            else
            {
                // Generate GUID for new Author
                author.AuthorId = Guid.NewGuid();
                // Add new Author to the database
                await dbContext.Authors.AddAsync(author);
            }

            if (existingGenre != null)
            {
                // Use the existing genre's ID
                genre.GenreId = existingGenre.GenreId;
            }
            else
            {
                // Generate GUID for new Genre
                genre.GenreId = Guid.NewGuid();
                // Add new Genre to the database
                await dbContext.Genres.AddAsync(genre);
            }

            // Add Book to the database
            await dbContext.Books.AddAsync(book);

            // Create a new BookAuthor entity
            var bookDetails = new BookDetails
            {
                BookDetailsId = Guid.NewGuid(),
                BookId = book.BookId,
                AuthorId = author.AuthorId,
                GenreId = genre.GenreId,
            };

            // Add BookAuthor to the database
            await dbContext.BookDetails.AddAsync(bookDetails);

            // Save changes to the database
            await dbContext.SaveChangesAsync();

            return book;
        }

        public async Task<bool> DeleteBookAsync(Guid id)
        {
            var bookEntity = await dbContext.Books.FindAsync(id);
            var bookDetailsEntity = await dbContext.BookDetails.FirstOrDefaultAsync(bookEntity => bookEntity.Book.BookId == id);
            // If the book is not found, return false
            if (bookEntity == null || bookDetailsEntity == null)
            {
                return false;
            }
            dbContext.Books.Remove(bookEntity);
            dbContext.BookDetails.Remove(bookDetailsEntity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<BookDetails>> GetAllBooksDetailsAsync(string? status, string? sortBy, bool? isAscending)
        {
            var bookDetailsList = dbContext.BookDetails.AsQueryable();
            //Filtering
            if (!string.IsNullOrWhiteSpace(status))
            {
                var queryStatus = Enum.Parse<BookStatus>(status);
                bookDetailsList = bookDetailsList.Where(x => x.Book.Status == queryStatus);
            }
            //Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    bookDetailsList = isAscending == true ? bookDetailsList.OrderBy(x => x.Book.Title) : bookDetailsList.OrderByDescending(x => x.Book.Title);
                }
                else if (sortBy.Equals("Author", StringComparison.OrdinalIgnoreCase))
                {
                    bookDetailsList = isAscending == true ? bookDetailsList.OrderBy(x => x.Author.FirstName) : bookDetailsList.OrderByDescending(x => x.Author.FirstName);
                }
                else if (sortBy.Equals("Genre", StringComparison.OrdinalIgnoreCase))
                {
                    bookDetailsList = isAscending == true ? bookDetailsList.OrderBy(x => x.Genre.Name) : bookDetailsList.OrderByDescending(x => x.Genre.Name);
                }
            }
            return await bookDetailsList.ToListAsync();
        }

        public async Task<BookDetails?> GetBookDetailsByIdAsync(Guid id)
        {
            var bookDetailsEntity =  await dbContext.BookDetails
                .Include(b => b.Book)
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(x => x.BookId == id);

            if(bookDetailsEntity == null)
            {
                return null;
            }
            return bookDetailsEntity;
        }

        public async Task<Book?> UpdateBookAsync(Guid id, Book book)
        {
            var bookEntity = await dbContext.Books.FindAsync(id);
            if (bookEntity == null)
            {
                return null;
            }
            bookEntity.Title = book.Title;
            bookEntity.ISBN = book.ISBN;
            bookEntity.PublicationYear = book.PublicationYear;
            bookEntity.TotalCopies = book.TotalCopies;
            bookEntity.AvailableCopies = book.AvailableCopies;
            bookEntity.Status = book.Status;
            bookEntity.Publisher = book.Publisher;
            bookEntity.CoverImageURL = book.CoverImageURL;
            bookEntity.Description = book.Description;
            bookEntity.Language = book.Language;
            bookEntity.Pages = book.Pages;
            bookEntity.UpdatedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();
            return bookEntity;
        }

        public async Task<BookDetails?> UpdateBookDetailsAsync(Guid id, Book book, Author author, Genre genre)
        {
            var bookDetailsEntity = await dbContext.BookDetails
                .Include(b => b.Book)
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(x => x.BookId == id);

            if (bookDetailsEntity == null)
            {
                return null;
            }

            // Check if the author already exists
            var existingAuthor = await dbContext.Authors
                .FirstOrDefaultAsync(a => a.FirstName == author.FirstName && a.LastName == author.LastName);

            // Check if the genre already exists
            var existingGenre = await dbContext.Genres
                .FirstOrDefaultAsync(g => g.Name == genre.Name);

            if (existingAuthor != null)
            {
                // Use the existing author's ID
                author.AuthorId = existingAuthor.AuthorId;
            }
            else
            {
                // Generate GUID for new Author
                author.AuthorId = Guid.NewGuid();
                // Add new Author to the database
                await dbContext.Authors.AddAsync(author);
            }

            if (existingGenre != null)
            {
                // Use the existing genre's ID
                genre.GenreId = existingGenre.GenreId;
            }
            else
            {
                // Generate GUID for new Genre
                genre.GenreId = Guid.NewGuid();
                // Add new Genre to the database
                await dbContext.Genres.AddAsync(genre);
            }

            // Update Book entity
            bookDetailsEntity.Book.Title = book.Title;
            bookDetailsEntity.Book.ISBN = book.ISBN;
            bookDetailsEntity.Book.PublicationYear = book.PublicationYear;
            bookDetailsEntity.Book.TotalCopies = book.TotalCopies;
            bookDetailsEntity.Book.AvailableCopies = book.AvailableCopies;
            bookDetailsEntity.Book.Status = book.Status;
            bookDetailsEntity.Book.Publisher = book.Publisher;
            bookDetailsEntity.Book.CoverImageURL = book.CoverImageURL;
            bookDetailsEntity.Book.Description = book.Description;
            bookDetailsEntity.Book.Language = book.Language;
            bookDetailsEntity.Book.Pages = book.Pages;
            bookDetailsEntity.Book.UpdatedDate = DateTime.UtcNow;
            // Update Author and Genre entities
            bookDetailsEntity.Author.FirstName = author.FirstName;
            bookDetailsEntity.Author.LastName = author.LastName;
            bookDetailsEntity.Genre.Name = genre.Name;
            bookDetailsEntity.Genre.Description = genre.Description;

            // Update BookDetails entity
            bookDetailsEntity.BookId = book.BookId;
            bookDetailsEntity.AuthorId = author.AuthorId;
            bookDetailsEntity.GenreId = genre.GenreId;

            // Save changes to the database
            await dbContext.SaveChangesAsync();

            return bookDetailsEntity;

        }
    }
}
