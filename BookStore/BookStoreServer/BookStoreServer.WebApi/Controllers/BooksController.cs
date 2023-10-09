using BookStoreServer.WebApi.Dtos;
using BookStoreServer.WebApi.Models;
using GSF.FuzzyStrings;
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

        if (!string.IsNullOrEmpty(request.Search))
        {
            newBooks = newBooks
            .Where(c => c.Title.ApproximatelyEquals(request.Search, FuzzyStringComparisonOptions.UseJaccardDistance, FuzzyStringComparisonTolerance.Strong) ||
                        c.Author.ApproximatelyEquals(request.Search, FuzzyStringComparisonOptions.UseJaccardDistance, FuzzyStringComparisonTolerance.Strong) ||
                        c.ISBN.ApproximatelyEquals(request.Search, FuzzyStringComparisonOptions.UseJaccardDistance, FuzzyStringComparisonTolerance.Strong))
            .ToList();
        }

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
    public static List<Book> Books = new();
    public static List<Category> Categories = new();
    public static List<BookCategory> BookCategories = new();
}