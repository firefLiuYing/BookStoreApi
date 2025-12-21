using BookStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(BookStoreContext context):ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> Get()
    {
        return await context.Order.ToListAsync();
    }
}