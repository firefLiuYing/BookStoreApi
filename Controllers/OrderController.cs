using BookStoreApi.Models;
using BookStoreApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class OrderController(BookStoreContext context): ControllerBase
{
    private readonly BookOrderService _bookOrderService = new(context);
    [HttpPost("order")]
    public async Task<IActionResult> order ([FromBody]BookOrderService.OrderInfo orderInfo)
    {
        Console.WriteLine("BookOrderService.OrderInfo 被调用");
        var result = await _bookOrderService.order(orderInfo);
        if(result.Success) return Ok(result);
        return BadRequest(result);
    }
}