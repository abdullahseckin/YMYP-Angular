using BookStoreServer.WebApi.Dtos;
using BookStoreServer.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BooksController : ControllerBase
{
    public BooksController()
    {
    }

    [HttpPost]
    public IActionResult GetAll(RequestDto request)
    {
        ResponseDto<List<Book>> response = new();
        string replaceSearch = request.Search.Replace("İ", "i").ToLower();
        var newBooks = new List<Book>();

        if (request.CategoryId != null)
        {
            newBooks = SeedData.BookCategories
           .Where(p => p.CategoryId == request.CategoryId)
           .Select(s => s.Book)
           .ToList();
        }
        else
        {
            newBooks = SeedData.Books;
        }

        newBooks = newBooks
            .Where(p => p.Title.ToLower().Contains(replaceSearch) ||
                        p.Author.ToLower().Contains(replaceSearch) ||
                        p.ISBN.ToLower().Contains(replaceSearch))
            .ToList();

        response.Data = newBooks
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();
        response.PageNumber = request.PageNumber;
        response.PageSize = request.PageSize;
        response.TotalPageCount = (int)Math.Ceiling(newBooks.Count / (double)request.PageSize);
        response.IsFirstPage = request.PageNumber == 1;
        response.IsLastPage = request.PageNumber == response.TotalPageCount;

        return Ok(response);
    }
}

public static class SeedData
{
    public static List<Book> Books = new BookService().CreateSeedBookData();
    public static List<Category> Categories = new BookService().CreateCategories();
    public static List<BookCategory> BookCategories = new BookService().CreateBookCategories();
}

public class BookService
{
    private List<Book> books = new();
    private List<Category> categories = new();
    private List<BookCategory> bookCategories = new();

    public List<Book> CreateSeedBookData()
    {
        for (int i = 0; i < 100; i++)
        {
            var book = new Book()
            {
                Id = i + 1,
                Title = $"Kitap {i + 1}",
                Author = "Yazar " + (i + 1),
                Summary = "",
                CoverImageUrl = "https://m.media-amazon.com/images/I/41BKx1AxQWL.jpg",
                CreateAt = DateTime.Now,
                IsActive = true,
                ISBN = "978-0321125217",
                Price = 5 * (i + 1),
                Quantity = i + 1
            };

            books.Add(book);
        }

        return books;
    }

    public List<Category> CreateCategories()
    {
        for (int i = 0; i < 10; i++)
        {
            var category = new Category()
            {
                Id = i + 1,
                Name = $"Kategori {i + 1}",
                IsActive = true,
                IsDeleted = false
            };

            categories.Add(category);
        }

        return categories;
    }

    public List<BookCategory> CreateBookCategories()
    {
        int id = 0;
        Random random = new();
        foreach (var book in SeedData.Books)
        {
            id++;
            var bookCategory = new BookCategory()
            {
                Id = id,
                BookId = book.Id,
                Book = book,
                CategoryId = random.Next(1, 10)
            };
            bookCategories.Add(bookCategory);
        }

        return bookCategories;
    }
}