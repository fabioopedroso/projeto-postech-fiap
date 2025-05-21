using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGamesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "CommonUser")]

public class CheckoutController : ControllerBase
{
    private readonly ICheckoutAppService _checkoutAppService;

    public CheckoutController(ICheckoutAppService checkoutAppService)
    {
        _checkoutAppService = checkoutAppService;
    }

    [HttpPost]
    public async Task<IActionResult> CheckoutCart()
    {
        var orderResult = await _checkoutAppService.CheckoutCart();
        return Ok(orderResult);
    }
}
