using BookStoreApi.Models;
using BookStoreApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController(BookStoreContext context) : ControllerBase
{
    private readonly BookMangeSevice _bookMangeSevice = new(context);

    [HttpPost("query")]
    public async Task<IActionResult> Query()
    {
        Console.WriteLine("_bookMangeSevice.query 被调用");
        var result = await _bookMangeSevice.query();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
    
    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody]List<BookMangeSevice.QueryDto> queryDto)
    {
        Console.WriteLine("_bookMangeSevice.update 被调用");
        var result = await _bookMangeSevice.update(queryDto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
}