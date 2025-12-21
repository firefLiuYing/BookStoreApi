namespace BookStoreApi.Models;

public class StockRecord
{
    public int BookId { get; set; }
    public int Count { get; set; }
    public DateTime UpdateTime { get; set; }
}