using Lesson14.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lesson19.Auth.Controllers;

[ApiController]
[Route("cart")]
[Authorize]
public class CartController : ControllerBase
{
    [HttpGet("get")]
    public Cart GetCart()
    {
        return new Cart();
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateCart(Cart cart)
    {
        return Ok();
    }
}