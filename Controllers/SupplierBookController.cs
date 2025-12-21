using BookStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplierBookController(BookStoreContext  context):ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupplierBook>>> Get()
    {
        return await context.SupplierBook.ToListAsync();
    }
}