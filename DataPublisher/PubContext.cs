using Microsoft.EntityFrameworkCore;
using PublisherDomain;
using System.Net;

namespace PublisherData
{
    public class PubContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source= (localdb)\\MSSQLLocalDB;Initial Catalog=PublisherDB;")
            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) //desactive le tracking des modifs
            ;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1,FirstName = "Jeff",LastName = "olsen"});

            var authors = new Author[]
            {
                new Author { Id = 2,FirstName = "Charles",LastName = "Duhigg"},
                new Author { Id = 3,FirstName = "Vitor",LastName = "Hugo"},
                new Author { Id = 4,FirstName = "Emile",LastName = "Zola"},
                new Author { Id = 5,FirstName = "Pablo",LastName = "Coelho"}

            };
            modelBuilder.Entity<Author>().HasData(authors);

            var books = new Book[]
            {
                new Book{BookId = 1,AuthorId = 1,Title = "The slight Edge",PublishDate = new DateOnly(1990,1,1),BasePrice = 17.5m},
                new Book{BookId = 2,AuthorId = 5,Title = "L'alchimiste",PublishDate = new DateOnly(1998,1,1),BasePrice = 19.2m},
                new Book{BookId = 3,AuthorId = 5,Title = "Onzes minutes",PublishDate = new DateOnly(2000,8,15),BasePrice = 10.25m}
            };
            modelBuilder.Entity<Book>().HasData(books);

        }
    }



}
