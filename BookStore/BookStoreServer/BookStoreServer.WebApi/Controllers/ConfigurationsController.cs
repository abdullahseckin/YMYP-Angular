using BookStoreServer.WebApi.Context;
using BookStoreServer.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ConfigurationsController : ControllerBase
{
    private readonly AppDbContext context = new();

    [HttpGet]
    public IActionResult SeedData()
    {
        List<Category> categories = new();
        for (int i = 0; i < 10; i++)
        {
            var category = new Category()
            {
                Name = $"Category {i}",
                IsActive = true,
                IsDeleted = false
            };
            categories.Add(category);
        }

        context.Categories.AddRange(categories);
        context.SaveChanges();

        List<Book> books = new();
        for (int i = 0; i < 100; i++)
        {
            var book = new Book()
            {
                Title = $"Book {i}",
                Author = $"Author {i}",
                Summary = $"Summary {i}",
                CoverImageUrl = $"https://m.media-amazon.com/images/I/71W4dU3uOZL._AC_UF1000,1000_QL80_.jpg",
                Price = new(i * 2, "₺"),
                Quantity = i * 1,
                IsActive = true,
                IsDeleted = false,
                ISBN = $"ISBN {i}",
                CreateAt = DateTime.Now
            };
            books.Add(book);
        }
        context.Books.AddRange(books);
        context.SaveChanges();

        List<BookCategory> bookCategories = new();
        foreach (var book in books)
        {
            var bookCategory = new BookCategory()
            {
                BookId = book.Id,
                CategoryId = categories[new Random().Next(0, categories.Count)].Id
            };

            bookCategories.Add(bookCategory);
        }

        context.BookCategories.AddRange(bookCategories);

        context.SaveChanges();

        return NoContent();
    }
}