using BookStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PurchaseBookSupplierController(BookStoreContext  context):ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PurchaseBookSupplier>>> Get()
    {
        return await context.PurchaseBookSupplier.ToListAsync();
    }    
}