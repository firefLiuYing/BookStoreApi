namespace BookStoreApi.Models;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime? EstablishTime { get; set; }
    public string? BossName { get; set; }
    public string? Phone { get; set; }
}