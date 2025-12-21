namespace BookStoreApi.Models;

public class PurchaseBookSupplier
{
    public int PurchaseId { get; set; }
    public int BookId { get; set; }
    public int SupplierId { get; set; }
    public int Count { get; set; }
}