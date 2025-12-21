using BookStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CreditController(BookStoreContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Credit>>> Get()
    {
        return await context.Credit.ToListAsync();
    }
}