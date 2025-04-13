using LMS.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Data
{
    public class LMSDbContext : DbContext
    {
        public LMSDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }


        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Member> Members { get; set; }


        public DbSet<BookBorrowing> BookBorrowings { get; set; }
        public DbSet<Fine> Fines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookBorrowing>()
                .HasOne(b => b.CheckedOutByUser)
                .WithMany(u => u.BookBorrowings)
                .HasForeignKey(b => b.CheckedOutByUserId)
                .OnDelete(DeleteBehavior.NoAction); // Specify NO ACTION for DELETE

            modelBuilder.Entity<BookBorrowing>()
                .HasOne(b => b.Librarian)
                .WithMany()
                .HasForeignKey(b => b.LibrarianUserId)
                .OnDelete(DeleteBehavior.NoAction); // Specify NO ACTION for DELETE
        }
    }
}
