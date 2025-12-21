namespace BookStoreApi.Models;

public class Customer
{
    public int Id { get; set; }
    public string NickName { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public decimal Balance { get; set; }
    public int Credit{get; set;}
    public string Email { get; set; }
    public string Phone { get; set; }
}