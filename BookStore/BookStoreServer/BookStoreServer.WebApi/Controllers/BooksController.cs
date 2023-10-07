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
        var newBooks = SeedData.Books
            .Where(x =>
                x.Title.Replace("İ", "i").ToLower().Contains(replaceSearch) ||
                x.Author.Replace("İ", "i").ToLower().Contains(replaceSearch)
             )
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
}

public class BookService
{
    private List<Book> books = new();

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
}