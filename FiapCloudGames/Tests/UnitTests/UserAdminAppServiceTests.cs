using Application.DTOs.User.Signatures;
using Application.Exceptions;
using Application.Interfaces;
using Application.Services;
using Core.Enums;
using Core.Interfaces.Repository;
using Core.ValueObjects;
using Moq;
using System.Reflection;

public class UserAdminAppServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUserAppService> _userAppServiceMock;
    private readonly UserAdminAppService _service;

    public UserAdminAppServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userAppServiceMock = new Mock<IUserAppService>();
        _service = new UserAdminAppService(_userRepositoryMock.Object, _userAppServiceMock.Object);
    }

    [Fact]
    public async Task CreateUser_Should_Call_Register_For_CommonUser()
    {
        var dto = new CreateUserDto { UserName = "user", Email = "user@email.com", Password = "pass", Role = "user" };

        await _service.CreateUser(dto);

        _userAppServiceMock.Verify(x => x.Register(It.Is<RegisterDto>(r =>
            r.UserName == dto.UserName &&
            r.Email == dto.Email &&
            r.Password == dto.Password)), Times.Once);

        _userRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task CreateUser_Should_Create_AdminUser()
    {
        var dto = new CreateUserDto { UserName = "admin", Email = "admin@email.com", Password = "pass", Role = "admin" };

        await _service.CreateUser(dto);

        _userRepositoryMock.Verify(x => x.CreateAsync(It.Is<User>(u =>
            u.UserName == dto.UserName &&
            u.Email.Address == dto.Email &&
            u.UserType == UserType.Administrator)), Times.Once);

        _userAppServiceMock.Verify(x => x.Register(It.IsAny<RegisterDto>()), Times.Never);
    }

    [Fact]
    public async Task GetUsers_Should_Return_UserDtos()
    {
        var users = new List<User>
        {
            new User("user1", new Email("a@a.com"), new Password("pass"), UserType.CommonUser),
            new User("user2", new Email("b@b.com"), new Password("pass"), UserType.Administrator)
        };
        _userRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(users);

        var result = await _service.GetUsers();

        Assert.Equal(2, result.Count());
        Assert.Contains(result, u => u.UserName == "user1" && u.Email == "a@a.com");
        Assert.Contains(result, u => u.UserName == "user2" && u.Email == "b@b.com");
    }

    [Fact]
    public async Task GetUserById_Should_Return_UserDto()
    {
        var user = new User("user", new Email("a@a.com"), new Password("pass"), UserType.CommonUser);
        typeof(User).GetProperty("Id").SetValue(user, 1);
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(user);

        var result = await _service.GetUserById(1);

        Assert.Equal(1, result.Id);
        Assert.Equal("user", result.UserName);
        Assert.Equal("a@a.com", result.Email);
    }

    [Fact]
    public async Task GetUserById_Should_Throw_When_NotFound()
    {
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((User)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetUserById(1));
    }

    [Fact]
    public async Task PromoteUser_Should_Set_UserType_Administrator()
    {
        var user = new User("user", new Email("a@a.com"), new Password("pass"), UserType.CommonUser);
        typeof(User).GetProperty("Id").SetValue(user, 1);
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(user);

        await _service.PromoteUser(1);

        Assert.Equal(UserType.Administrator, user.UserType);
        _userRepositoryMock.Verify(x => x.UpdateAsync(user), Times.Once);
    }

    [Fact]
    public async Task PromoteUser_Should_Throw_When_NotFound()
    {
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((User)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.PromoteUser(1));
    }

    [Fact]
    public async Task DemoteUser_Should_Set_UserType_CommonUser()
    {
        var user = new User("user", new Email("a@a.com"), new Password("pass"), UserType.Administrator);
        typeof(User).GetProperty("Id").SetValue(user, 1);
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(user);

        await _service.DemoteUser(1);

        Assert.Equal(UserType.CommonUser, user.UserType);
        _userRepositoryMock.Verify(x => x.UpdateAsync(user), Times.Once);
    }

    [Fact]
    public async Task DemoteUser_Should_Throw_When_NotFound()
    {
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((User)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DemoteUser(1));
    }

    [Fact]
    public async Task SetUserActiveStatus_Should_Update_IsActive()
    {
        var user = new User("user", new Email("a@a.com"), new Password("pass"), UserType.CommonUser);
        typeof(User).GetProperty("Id").SetValue(user, 1);
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(user);

        var dto = new SetUserActiveStatusDto { UserId = 1, IsActive = false };

        await _service.SetUserActiveStatus(dto);

        Assert.False(user.IsActive);
        _userRepositoryMock.Verify(x => x.UpdateAsync(user), Times.Once);
    }

    [Fact]
    public async Task SetUserActiveStatus_Should_Throw_When_NotFound()
    {
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((User)null);

        var dto = new SetUserActiveStatusDto { UserId = 1, IsActive = false };

        await Assert.ThrowsAsync<NotFoundException>(() => _service.SetUserActiveStatus(dto));
    }

    [Theory]
    [InlineData("admin", UserType.Administrator)]
    [InlineData("user", UserType.CommonUser)]
    public void ParseUserType_Should_Return_Correct_UserType(string role, UserType expected)
    {
        var method = typeof(UserAdminAppService).GetMethod("ParseUserType", BindingFlags.NonPublic | BindingFlags.Instance);
        var result = method.Invoke(_service, new object[] { role });
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseUserType_Should_Throw_On_Invalid_Role()
    {
        var method = typeof(UserAdminAppService).GetMethod("ParseUserType", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.Throws<TargetInvocationException>(() => method.Invoke(_service, new object[] { "invalid" }));
    }
}