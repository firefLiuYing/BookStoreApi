using BookStoreApi.Models;
using BookStoreApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Service;

public class BookManageSevice(BookStoreContext  context)
{
    public async Task<ApiResponse<List<QueryDto>>> query()
    {
        var response = new ApiResponse<List<QueryDto>>();
        try
        {
            var list = await context.Book
                .Join(
                    context.BookInventory,                // 连接 OrderBook 表
                    b => b.Id,                        // Order 表主键
                    bi => bi.BookId,                 // OrderBook 外键
                    (b, bi) => new QueryDto 
                    {
                        bookId = b.Id,
                        isbn = b.ISBN,
                        name = b.Name,
                        author = b.Author,
                        publisher = b.Publisher,
                        price = b.Price,
                        keyWord = b.Keyword ?? " ",
                        supplier = b.Supplier ?? " ",
                        catalog = b.Catalog ?? " ",
                        cover = b.Cover ?? " ",
                        inventory = bi.Inventory
                    }
                )
                .OrderBy(x => x.bookId)  // 按订单日期倒序
                .ToListAsync();
            
            response.Success = true;
            response.Data = list;
        }
        catch (Exception ex)
        {
            // 捕获数据库查询异常
            response.Success = false;
            response.Message = "发的撒娇occurred while fetching books: " + ex.Message;
        }

        return response;
    }

    public async Task<ApiResponse<List<QueryDto>>> update (List<QueryDto> books)
    {
        var response = new ApiResponse<List<QueryDto>>();
        
        var bookIds = books.Select(b => b.bookId).ToList();

        var dbBooks = await context.Book
            .Where(b => bookIds.Contains(b.Id))
            .ToListAsync();
        
        var dbBookInventorys = await context.BookInventory
            .Where(bi => bookIds.Contains(bi.BookId))
            .ToListAsync();

        foreach (var dbBook in dbBooks)
        {
            var dto = books.First(b => b.bookId == dbBook.Id);
            dbBook.ISBN = dto.isbn;
            dbBook.Name = dto.name;
            dbBook.Author = dto.author;
            dbBook.Publisher = dto.publisher;
            dbBook.Price = dto.price;
            dbBook.Keyword = dto.keyWord;
            dbBook.Supplier = dto.supplier;
            dbBook.Cover = dto.cover;
            dbBook.Catalog = dto.catalog;
        }
        
        foreach (var dbBookInventory in dbBookInventorys)
        {
            var dto = books.First(b => b.bookId == dbBookInventory.BookId);
            dbBookInventory.Inventory = dto.inventory;
        }

        await context.SaveChangesAsync();
        response.Success = true;
        response.Data = null;

        return response;
    }

    public class QueryDto
    {
        public int bookId { get; set; }
        public string isbn { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public string publisher { get; set; }
        public decimal price { get; set; }
        public string keyWord { get; set; }
        public string supplier { get; set; }
        public string catalog { get; set; }
        public string cover { get; set; }
        public int inventory{ get; set; }
    }
}