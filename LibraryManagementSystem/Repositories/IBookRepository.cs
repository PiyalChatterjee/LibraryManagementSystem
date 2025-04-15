using LMS.API.Models.Domain;

namespace LMS.API.Repositories
{
    public interface IBookRepository
    {
        Task<Book> CreateAsync(Book book, Author author, Genre genre);
        Task<BookDetails?> GetBookDetailsByIdAsync(Guid id);
        Task<List<BookDetails>> GetAllBooksDetailsAsync(string? status, string? sortBy, bool? isAscending);
        Task<Book?> UpdateBookAsync(Guid id, Book book);
        Task<BookDetails?> UpdateBookDetailsAsync(Guid id, Book book, Author author, Genre genre);
        Task<bool> DeleteBookAsync(Guid id);
    }
}
