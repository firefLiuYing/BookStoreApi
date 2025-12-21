using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Models;

public class BookStoreContext:DbContext
{
    public BookStoreContext(DbContextOptions<BookStoreContext> options):base(options){}
    
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().ToTable("book");
    }
}