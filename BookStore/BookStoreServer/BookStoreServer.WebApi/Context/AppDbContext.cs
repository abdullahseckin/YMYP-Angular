using BookStoreServer.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.WebApi.Context;

public sealed class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=TUGAY\\SQLEXPRESS;Initial Catalog=YMYP_BookStoreDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookCategory> BookCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().OwnsOne(p => p.Price, price =>
        {
            price.Property(p => p.Value).HasColumnType("money");
            price.Property(p => p.Currency).HasMaxLength(5); // Assuming you want a max length for Currency
        });//Value Object

        modelBuilder.Entity<BookCategory>().HasKey(p => new { p.BookId, p.CategoryId });
        //Composite Key

        //Domain Driven Design
        //Value Object

        //Note: Value object kullanıyorsak seed data yapamıyoruz.

        //Makale konuları
        //1) Fuzzy String
        //2) Seed Data
        //3) Value Object
    }
}