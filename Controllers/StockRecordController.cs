using BookStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class StockRecordController(BookStoreContext  context):ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StockRecord>>> Get()
    {
        return await context.StockRecord.ToListAsync();
    }
}