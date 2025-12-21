namespace BookStoreApi.Models;

public class Purchase
{
    public int Id { get; set; }
    public bool Arrived { get; set; }
    public DateTime CreateTime { get; set; }
}