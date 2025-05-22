namespace Application.Contracts;
public interface ICurrentUseService
{
    int UserId { get; }
    string UserName { get; }
    string Email { get; }
    string UserType { get; }
    bool IsAuthenticated { get; }
}
