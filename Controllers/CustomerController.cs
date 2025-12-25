using BookStoreApi.Models;
using BookStoreApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(BookStoreContext context): ControllerBase
{
    private readonly CustomerManageService _customerManageService=new(context);
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> Get()
    {
        return await context.Customer.ToListAsync();
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] CustomerManageService.CustomerInformation request)
    {
        var result = await _customerManageService.Update(request);
        if(result.Success) return Ok(result);
        return BadRequest(result);
    }
}