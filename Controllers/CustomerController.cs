using BookStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(BookStoreContext context): ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> Get()
    {
        return await context.Customer.ToListAsync();
    }
}