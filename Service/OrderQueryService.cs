using BookStoreApi.Models;
using BookStoreApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Service;

public class OrderQueryService(BookStoreContext  context)
{
    public async Task<ApiResponse<List<QueryDto>>> query(CustomerInfo customerInfo)
    {
        var response = new ApiResponse<List<QueryDto>>();
        
        var list = await context.Order
            .Where(o => o.CustomerId == customerInfo.Id)   // 筛选当前用户订单
            .Join(
                context.OrderBook,                // 连接 OrderBook 表
                o => o.Id,                        // Order 表主键
                ob => ob.OrderId,                 // OrderBook 外键
                (o, ob) => new QueryDto 
                {
                    OrderId = o.Id,
                    CustomerId = o.CustomerId,
                    Address = o.Address,
                    Amount = o.Amount,
                    Paid = o.Paid, 
                    Shipped = o.Shipped,
                    CreatedTime = o.CreatedTime,
                    BookId = ob.BookId,
                    Count = ob.Count
                }
            )
            .OrderByDescending(x => x.CreatedTime)  // 按订单日期倒序
            .ToListAsync();

        response.Success = true;
        response.Data = list;

        return response;
    }
    
    public class CustomerInfo 
    {
        public int Id { get; set; }
    }

    public class QueryDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Address { get; set; }
        public decimal Amount { get; set; }
        public bool Paid { get; set; }
        public bool Shipped { get; set; }
        public DateTime CreatedTime { get; set; }
        public int BookId { get; set; }
        public int Count { get; set; }
    }
}