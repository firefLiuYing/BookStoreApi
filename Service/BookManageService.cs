using BookStoreApi.Models;
using BookStoreApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Service;

public class BookMangeSevice(BookStoreContext  context)
{
    public async Task<ApiResponse<List<QueryDto>>> query()
    {
        var response = new ApiResponse<List<QueryDto>>();
        
        var list = await context.Book
            .Join(
                context.BookInventory,                // 连接 OrderBook 表
                b => b.Id,                        // Order 表主键
                bi => bi.BookId,                 // OrderBook 外键
                (b, bi) => new QueryDto 
                {
                    BookId = b.Id,
                    ISBN = b.ISBN,
                    Name = b.Name,
                    Author = b.Author,
                    Publisher = b.Publisher,
                    Price = b.Price,
                    KeyWord = b.Keyword,
                    Supplier = b.Supplier,
                    Catalog = b.Catalog,
                    Cover = b.Cover,
                    inventory = bi.Inventory
                }
            )
            .OrderByDescending(x => x.BookId)  // 按订单日期倒序
            .ToListAsync();

        response.Success = true;
        response.Data = list;

        return response;
    }

    public async Task<ApiResponse<List<QueryDto>>> update (List<QueryDto> books)
    {
        var response = new ApiResponse<List<QueryDto>>();
        
        var bookIds = books.Select(b => b.BookId).ToList();

        var dbBooks = await context.Book
            .Where(b => bookIds.Contains(b.Id))
            .ToListAsync();
        
        var dbBookInventorys = await context.BookInventory
            .Where(bi => bookIds.Contains(bi.BookId))
            .ToListAsync();

        foreach (var dbBook in dbBooks)
        {
            var dto = books.First(b => b.BookId == dbBook.Id);
            dbBook.ISBN = dto.ISBN;
            dbBook.Name = dto.Name;
            dbBook.Author = dto.Author;
            dbBook.Publisher = dto.Publisher;
            dbBook.Price = dto.Price;
            dbBook.Keyword = dto.KeyWord;
            dbBook.Supplier = dto.Supplier;
            dbBook.Cover = dto.Cover;
            dbBook.Catalog = dto.Catalog;
        }
        
        foreach (var dbBookInventory in dbBookInventorys)
        {
            var dto = books.First(b => b.BookId == dbBookInventory.BookId);
            dbBookInventory.Inventory = dto.inventory;
        }

        await context.SaveChangesAsync();
        response.Success = true;
        response.Data = null;

        return response;
    }

    public class QueryDto
    {
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public decimal Price { get; set; }
        public string KeyWord { get; set; }
        public string Supplier { get; set; }
        public string Catalog { get; set; }
        public string Cover { get; set; }
        public int inventory{ get; set; }
    }
}