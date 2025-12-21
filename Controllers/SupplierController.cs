using BookStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplierController(BookStoreContext  context):ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Supplier>>> Get()
    {
        return await context.Supplier.ToListAsync();
    }
}