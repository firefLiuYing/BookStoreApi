using BookStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController:ControllerBase
{
    private readonly BookStoreContext _context;
    public BooksController(BookStoreContext context)=>_context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        return await _context.Books.ToListAsync();
    }
}