namespace BookStoreApi.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Address { get; set; }
    public decimal Amount { get; set; }
    public bool Paid { get; set; }
    public bool Shipped { get; set; }
    public DateTime CreatedTime { get; set; }
}