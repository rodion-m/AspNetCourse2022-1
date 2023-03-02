using Lesson14.Models;
using Lesson23.Filters.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lesson23.Filters.Controllers;

[LogResourceFilter]
[ApiController]
[Route("[controller]")]
[AddHeader("Api-Version", "1")]
public class OrderController : ControllerBase
{
    [HttpGet]
    [AppAuthFilter(Order = FilterOrder.AppAuthFilter1)]
    public IActionResult Index()
    {
        return Ok();
    }

    [HttpGet("my_action")]
    public IActionResult MyAction()
    {
        return Ok();
    }

    [CentralizedExceptionHandlingFilter(Order = (int) FilterOrder.AppAuthFilter1)]
    //[AppExceptionFilter1(Order = (int) Orders.Asd)]
    [HttpPost("offer")]
    public IActionResult Offer(Order order)
    {
        return Ok();
    }

    [CentralizedExceptionHandlingFilter(Order = 1)]
    [HttpGet("offer2", Name = "OfferGet")]
    public IActionResult Offer2(string username, int amount, bool callManager)
    {
        return Ok();
    }
    
    [ServiceFilter(typeof(AppAlwaysRunResultFilter))]
    [TypeFilter(typeof(AppAuthorizationFilter))]
    [UsefulAuthFilter]
    [HttpGet("get_account")]
    [Authorize]
    public IActionResult GetAccount()
    {
        return Ok();
    }
}