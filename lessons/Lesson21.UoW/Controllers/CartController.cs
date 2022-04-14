using Lesson14.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lesson21.UoW;

[Authorize]
[ApiController]
public class CartController : ControllerBase
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService;
    }

    public Task<Cart> GetCart()
    {
        return _cartService.GetCartForAccount(User.GetAccountId());
    }
}