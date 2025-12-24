using BookStoreApi.Models;
using BookStoreApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Service;

public class BookOrderService(BookStoreContext  context)
{
    public async Task<ApiResponse<OrderBookInfo>> Revise(OrderBookInfo orderBookInfo)
    {
        var response =new ApiResponse<OrderBookInfo>();
        if ( orderBookInfo?.BookId == null)
        {
            response.Success = false;
            response.Message = "某一栏为空";
            response.Data=null;
            return response;
        }

        var book_inventory = await context.BookInventory.FirstOrDefaultAsync(b => b.BookId == orderBookInfo.BookId);
        if (book_inventory == null)
        {
            response.Success = true;
            response.Message = "该用户不存在";
            response.Data = null;
            return response;
        }
        else
        {
            if(book_inventory.Inventory <= orderBookInfo.OrderAmount)
            response.Success = true;
            response.Message = "库存量不足，已为您发送订单，等待发货";
            response.Data = null;
            
            
            
            await context.SaveChangesAsync();
            return response;
        }
        
    }
    
    public class OrderBookInfo
    {
        public int BookId { get; set; }
        public int OrderAmount { get; set; }
    }
}