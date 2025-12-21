namespace BookStoreApi.Models;

public class Book
{
    public int Id { get; set; }
    public string ISBN { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public decimal Price { get; set; }
    public string? Keyword { get; set; }
    public string? Supplier { get; set; }
    public string? Catalog { get; set; }
    public string? Cover { get; set; }
}