using BookStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookInventoryController(BookStoreContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookInventory>>> Get()
    {
        return await context.BookInventory.ToListAsync();
    }
}