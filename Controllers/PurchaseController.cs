using BookStoreApi.Models;
using BookStoreApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PurchaseController(BookStoreContext context):ControllerBase
{
    private readonly PurchaseManageService  _purchaseManageService=new(context);
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Purchase>>> Get()
    {
        return await context.Purchase.ToListAsync();
    }

    [HttpGet("query")]
    public async Task<IActionResult> Query()
    {
        var result=await _purchaseManageService.Query();
        if(result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody]PurchaseManageService.PurchaseUpdateRequest  request)
    {
        var result = await _purchaseManageService.Update(request);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
}