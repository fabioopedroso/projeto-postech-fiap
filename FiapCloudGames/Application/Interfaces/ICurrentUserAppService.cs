namespace Application.Interfaces;
public interface ICurrentUserAppService
{
    int UserId { get; }
    string UserName { get; }
    string Email { get; }
    string UserType { get; }
    bool IsAuthenticated { get; }
}
