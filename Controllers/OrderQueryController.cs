using BookStoreApi.Models;
using BookStoreApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderQueryController(BookStoreContext context) : ControllerBase
{
    private readonly OrderQueryService _orderQueryService = new(context);

    [HttpPost("query")]
    public async Task<IActionResult> Query([FromBody] OrderQueryService.CustomerInfo customerInfo)
    {
        Console.WriteLine("OrderQueryService.query 被调用");
        var result = await _orderQueryService.query(customerInfo);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
}