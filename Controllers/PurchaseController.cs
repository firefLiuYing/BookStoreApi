using BookStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PurchaseController(BookStoreContext context):ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Purchase>>> Get()
    {
        return await context.Purchase.ToListAsync();
    }
}