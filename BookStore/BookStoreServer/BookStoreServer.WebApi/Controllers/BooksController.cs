using BookStoreServer.WebApi.Dtos;
using BookStoreServer.WebApi.Models;
using EntityFrameworkCorePagination.Nuget.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BooksController : ControllerBase
{
    private static List<Book> books = new();

    public BooksController()
    {
        books = new();

        for (int i = 0; i < 100; i++)
        {//ctrl + k + d
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
    }

    [HttpPost]
    public IActionResult GetAll(RequestDto request)
    {
        ResponseDto<List<Book>> response = new();
        string replaceSearch = request.Search.Replace("İ", "i").ToLower();
        response.Data = books
            .Where(x =>
                x.Title.Replace("İ", "i").ToLower().Contains(replaceSearch) ||
                x.Author.Replace("İ", "i").ToLower().Contains(replaceSearch)
             )
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();
        response.PageNumber = request.PageNumber;
        response.PageSize = request.PageSize;
        response.TotalPageCount = (int)Math.Ceiling(books.Count / (double)request.PageSize);
        response.IsFirstPage = request.PageNumber == 1;
        response.IsLastPage = request.PageNumber == response.TotalPageCount;

        return Ok(response);
    }
}