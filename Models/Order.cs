using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BookStoreApi.Models;

public class Order
{
    [Key]  // 主键
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // 自增
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Address { get; set; }
    public decimal Amount { get; set; }
    public bool Paid { get; set; }
    public bool Shipped { get; set; }
    public DateTime CreatedTime { get; set; }
}