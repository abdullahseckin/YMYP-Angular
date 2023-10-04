namespace BookStoreServer.WebApi.Models;

public sealed class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string CoverImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public int Quantity { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public string ISBN { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; } = DateTime.Now;
}