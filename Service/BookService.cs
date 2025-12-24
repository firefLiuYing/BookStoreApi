using BookStoreApi.Models;
using BookStoreApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Service;

public class BookService(BookStoreContext context)
{
    public async Task<ApiResponse<QueryResponse>> Query(QueryRequest request)
    {
        var response = new ApiResponse<QueryResponse>();
        if (request?.Context == null || request.Context == "")
        {
            response.Success = false;
            response.Message = "参数或查询内容不能为空";
            return response;
        }
        List<BookInfo> books;
        switch (request.QueryType)
        {
            case QueryType.Name:
                books = await context.Book.Where(book => book.Name.Contains(request.Context))
                    .Select(book => new BookInfo(book)).ToListAsync();
                break;
            case QueryType.Isbn:
                books=await context.Book.Where(book => book.ISBN.Contains(request.Context))
                .Select(book => new BookInfo(book)).ToListAsync();
                break;
            case QueryType.Publisher:
                books=await context.Book.Where(book => book.Publisher.Contains(request.Context))
                .Select(book => new BookInfo(book)).ToListAsync();
                break;
            case QueryType.Author:
                books=await context.Book.Where(book => book.Author.Contains(request.Context))
                .Select(book => new BookInfo(book)).ToListAsync();
                break;
            case QueryType.Keyword:
                books=await context.Book.Where(book => book.Keyword!=null&&book.Keyword.Contains(request.Context))
                .Select(book => new BookInfo(book)).ToListAsync();
                break;
            default:
                response.Success = false;
                response.Message = "查询类型不支持";
                return response;
        }
        response.Success = true;
        response.Message=$"查找到符合条件书籍数量为：{books.Count}";
        response.Data = new  QueryResponse(books);
        return response;
    }
    public class QueryRequest
    {
        public string Context { get; set; }
        public QueryType QueryType { get; set; }
    }

    public class QueryResponse
    {
        public List<BookInfo> Books { get; set; }

        public QueryResponse(IEnumerable<BookInfo> books)
        {
            Books=new();
            Books.AddRange(books);
        }
    }
    public class BookInfo(Book book)
    {
        public int Id { get; set; } = book.Id;
        public string ISBN { get; set; } = book.ISBN;
        public string Name { get; set; } = book.Name;
        public string Publisher { get; set; } = book.Publisher;
        public string Author { get; set; } = book.Author;
        public decimal Price { get; set; } = book.Price;
        public string? Keyword { get; set; } = book.Keyword;
        public string? Supplier { get; set; } = book.Supplier;
        public string? Catalog { get; set; } = book.Catalog;
        public string? Cover { get; set; } = book.Cover;
    }

    public enum QueryType
    {
        Name,
        Isbn,
        Publisher,
        Author,
        Keyword,
    }
}