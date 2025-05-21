using Application.DTOs.Checkout.Result;

namespace Application.Interfaces;
public interface ICheckoutAppService
{
    Task<OrderResultDto> CheckoutCart();
}
