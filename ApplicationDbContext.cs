using Microsoft.EntityFrameworkCore;
using CourseWork.Models;

namespace CourseWork
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Navigation(e => e.Author).AutoInclude();
            modelBuilder.Entity<Book>().Navigation(e => e.Form).AutoInclude();
            modelBuilder.Entity<Book>().Navigation(e => e.Content).AutoInclude();
            modelBuilder.Entity<Book>().Navigation(e => e.Genus).AutoInclude();
            modelBuilder.Entity<Book>().Navigation(e => e.Style).AutoInclude();
            modelBuilder.Entity<IssueHistory>().Navigation(e => e.Book).AutoInclude();
            modelBuilder.Entity<IssueHistory>().Navigation(e => e.User).AutoInclude();
            modelBuilder.Entity<Review>().Navigation(e => e.Book).AutoInclude();
            modelBuilder.Entity<Review>().Navigation(e => e.User).AutoInclude();
            modelBuilder.Entity<User>().Navigation(e => e.Role).AutoInclude();
            modelBuilder.Entity<Author>().Navigation(e => e.Books).AutoInclude();

            // Constraints
            modelBuilder.Entity<User>()
                .ToTable(t => t.HasCheckConstraint("Age", "Age > 0 AND Age < 120")
                .HasName("CK_User_Age"));
            modelBuilder.Entity<User>()
                .ToTable(t => t.HasCheckConstraint("Gender", "Gender = 'M' OR Gender = 'F'")
                .HasName("CK_User_Gender"));

            modelBuilder.Entity<Review>()
                .ToTable(t => t.HasCheckConstraint("Rating", "Rating > 0 AND Rating < 6")
                .HasName("CK_Review_Rating"));
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<IssueHistory> IssueHistories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EntryHistory> EntryHistories { get; set; }
    }
}