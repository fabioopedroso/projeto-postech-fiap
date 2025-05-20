using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services;
public class CurrentUserAppService : ICurrentUserAppService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserAppService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public int UserId
    {
        get
        {
            var claim = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return int.TryParse(claim?.Value, out var id) ? id : throw new UnauthorizedAccessException("Usuário não autenticado.");
        }
    }

    public string UserName =>
        _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? string.Empty;

    public string Email =>
        _httpContextAccessor.HttpContext?.User?.FindFirst("Email")?.Value ?? string.Empty;

    public string UserType =>
        _httpContextAccessor.HttpContext?.User?.FindFirst("UserType")?.Value ?? string.Empty;
}
