using BookStoreApi.Models;
using BookStoreApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Service;

public class BookOrderService(BookStoreContext  context)
{
    public async Task<ApiResponse<OrderInfo>> order(OrderInfo orderInfo)
    {
        var response =new ApiResponse<OrderInfo>();
        if ( orderInfo?.BookId == null)
        {
            response.Success = false;
            response.Message = "某一栏为空";
            response.Data=null;
            return response;
        }

        var book_inventory = await context.BookInventory.FirstOrDefaultAsync(b => b.BookId == orderInfo.BookId);
        if (book_inventory == null)
        {
            response.Success = false;
            response.Message = "该书籍不存在";
            response.Data = null;
            return response;
        }
        else
        {   
            var customer = await context.Customer.FirstOrDefaultAsync(c => c.Id == orderInfo.CustomerId);
            var credit = await context.Credit.FirstOrDefaultAsync(c => c.Level == customer.Credit);
            var book = await context.Book.FirstOrDefaultAsync(b => b.Id == orderInfo.BookId);
            
            if (customer == null || book == null || credit == null)
            {
                response.Success = false;
                response.Message = "对应信息缺失";
                response.Data = null;
                return response;
            }

            var amount = book.Price * (decimal)credit.Reduction * orderInfo.Count;
            
            if (book_inventory.Inventory <= orderInfo.Count)
            {
                response.Success = true;
                response.Message = "库存量不足，已为您发送订单，等待发货";
                
                var order = new Order()
                {
                    Address = customer.Address,
                    CustomerId = orderInfo.CustomerId,
                    Paid = false,
                    Shipped = false,
                    Amount = amount,
                    CreatedTime =  DateTime.Now
                };

                context.Order.Add(order);
                await context.SaveChangesAsync();
                
                var orderBook = new OrderBook()
                {
                    OrderId = order.Id,
                    BookId = book_inventory.BookId,
                    Count = orderInfo.Count
                };
                
                context.OrderBook.Add(orderBook);
                await context.SaveChangesAsync();

                response.Data = new OrderInfo()
                {
                    Amount =  amount
                };
                return response;
            }
            else
            {
                response.Success = true;
                response.Message = "库存量充足，已为您发送订单，立即发货";
            
                var order = new Order()
                {
                    Address = customer.Address,
                    CustomerId = orderInfo.CustomerId,
                    Paid = false,
                    Shipped = false,
                    Amount = amount,
                    CreatedTime =  DateTime.Now
                };

                context.Order.Add(order);
                await context.SaveChangesAsync();
                
                var orderBook = new OrderBook()
                {
                    OrderId = order.Id,
                    BookId = book_inventory.BookId,
                    Count = orderInfo.Count
                };
                
                context.OrderBook.Add(orderBook);
                await context.SaveChangesAsync();
                
                response.Data = new OrderInfo()
                {
                    Amount =  amount
                };
                return response;
            }
        }
        
    }
    
    public class OrderInfo
    {
        public int BookId { get; set; }
        public int Count { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }

    }
}