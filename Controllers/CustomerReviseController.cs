using BookStoreApi.Models;
using BookStoreApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CustomerReviseController(BookStoreContext context): ControllerBase
{
    private readonly CustomerReviseService _customerReviseService = new(context);
    [HttpPost("revise")]
    public async Task<IActionResult> Revise ([FromBody]CustomerReviseService.NewUserInfo newUserInfo)
    {
        Console.WriteLine("CustomerRevise.Revise ±»µ÷ÓÃ");
        var result = await _customerReviseService.Revise(newUserInfo);
        if(result.Success) return Ok(result);
        return BadRequest(result);
    }
}