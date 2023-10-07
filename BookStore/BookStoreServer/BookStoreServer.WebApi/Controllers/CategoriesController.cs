using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(SeedData.Categories);
    }
}