using BookStoreServer.WebApi.ValueObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreServer.WebApi.Models;

public sealed class Order
{    
    public int Id { get; set; }
    public string OrderNumber { get; set; } //16 hane ve unique olacak TNR2023000000001

    [ForeignKey("Book")]
    public int BookId { get; set; }
    public Book Book { get; set; }

    public Money Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentType { get; set; }
    public string PaymentNumber { get; set; }

    public static string GetNewOrderNumber()
    {
        //Todo: burayı değiştireceğiz
        return Guid.NewGuid().ToString();
    }
}
