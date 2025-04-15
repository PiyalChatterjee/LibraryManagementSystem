using LMS.API.Models.Domain;
using LMS.API.Models.Enums;
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
        public DbSet<BookDetails> BookDetails { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Member> Members { get; set; }


        public DbSet<BookBorrowing> BookBorrowings { get; set; }
        public DbSet<Fine> Fines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for Users into the database
            var users = new List<User>()
            {
                new User()
                {
                    UserId = Guid.Parse("b5f81d7d-49de-4508-aab5-3d598388f02f"),
                    FirstName = "Admin",
                    LastName = "User",
                    Username = "admin",
                    Password = "admin123", // In a real application, you should hash the password
                    Email = "admin@email.com",
                    Role = UserRoles.Admin,
                    Status = UserStatus.Active,
                    DateCreated = new DateTime(2025, 04, 14),
                }
            };

            modelBuilder.Entity<User>()
                .HasData(users);

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
