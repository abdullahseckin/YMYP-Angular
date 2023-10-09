namespace BookStoreServer.WebApi.Models;

public sealed class BookCategory
{
    //Composite Key
    public int BookId { get; set; }

    public Book Book { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}