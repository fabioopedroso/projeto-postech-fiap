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
    public async Task Given_CommonUser_When_CreateUser_Then_RegisterIsCalled()
    {
        // Given
        var dto = new CreateUserDto { UserName = "user", Email = "user@email.com", Password = "pass", Role = "user" };

        // When
        await _service.CreateUser(dto);

        // Then
        _userAppServiceMock.Verify(x => x.Register(It.Is<RegisterDto>(r =>
            r.UserName == dto.UserName &&
            r.Email == dto.Email &&
            r.Password == dto.Password)), Times.Once);

        _userRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task Given_AdminUser_When_CreateUser_Then_UserIsCreated()
    {
        // Given
        var dto = new CreateUserDto { UserName = "admin", Email = "admin@email.com", Password = "pass", Role = "admin" };

        // When
        await _service.CreateUser(dto);

        // Then
        _userRepositoryMock.Verify(x => x.CreateAsync(It.Is<User>(u =>
            u.UserName == dto.UserName &&
            u.Email.Address == dto.Email &&
            u.UserType == UserType.Administrator)), Times.Once);

        _userAppServiceMock.Verify(x => x.Register(It.IsAny<RegisterDto>()), Times.Never);
    }

    [Fact]
    public async Task Given_UsersExist_When_GetUsers_Then_ReturnsUserDtos()
    {
        // Given
        var users = new List<User>
        {
            new User("user1", new Email("a@a.com"), new Password("pass"), UserType.CommonUser),
            new User("user2", new Email("b@b.com"), new Password("pass"), UserType.Administrator)
        };
        _userRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(users);

        // When
        var result = await _service.GetUsers();

        // Then
        Assert.Equal(2, result.Count());
        Assert.Contains(result, u => u.UserName == "user1" && u.Email == "a@a.com");
        Assert.Contains(result, u => u.UserName == "user2" && u.Email == "b@b.com");
    }

    [Fact]
    public async Task Given_UserExists_When_GetUserById_Then_ReturnsUserDto()
    {
        // Given
        var user = new User("user", new Email("a@a.com"), new Password("pass"), UserType.CommonUser);
        typeof(User).GetProperty("Id").SetValue(user, 1);
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(user);

        // When
        var result = await _service.GetUserById(1);

        // Then
        Assert.Equal(1, result.Id);
        Assert.Equal("user", result.UserName);
        Assert.Equal("a@a.com", result.Email);
    }

    [Fact]
    public async Task Given_UserDoesNotExist_When_GetUserById_Then_ThrowsNotFoundException()
    {
        // Given
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((User)null);

        // When / Then
        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetUserById(1));
    }

    [Fact]
    public async Task Given_UserExists_When_PromoteUser_Then_UserTypeIsAdministrator()
    {
        // Given
        var user = new User("user", new Email("a@a.com"), new Password("pass"), UserType.CommonUser);
        typeof(User).GetProperty("Id").SetValue(user, 1);
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(user);

        // When
        await _service.PromoteUser(1);

        // Then
        Assert.Equal(UserType.Administrator, user.UserType);
        _userRepositoryMock.Verify(x => x.UpdateAsync(user), Times.Once);
    }

    [Fact]
    public async Task Given_UserDoesNotExist_When_PromoteUser_Then_ThrowsNotFoundException()
    {
        // Given
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((User)null);

        // When / Then
        await Assert.ThrowsAsync<NotFoundException>(() => _service.PromoteUser(1));
    }

    [Fact]
    public async Task Given_UserExists_When_DemoteUser_Then_UserTypeIsCommonUser()
    {
        // Given
        var user = new User("user", new Email("a@a.com"), new Password("pass"), UserType.Administrator);
        typeof(User).GetProperty("Id").SetValue(user, 1);
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(user);

        // When
        await _service.DemoteUser(1);

        // Then
        Assert.Equal(UserType.CommonUser, user.UserType);
        _userRepositoryMock.Verify(x => x.UpdateAsync(user), Times.Once);
    }

    [Fact]
    public async Task Given_UserDoesNotExist_When_DemoteUser_Then_ThrowsNotFoundException()
    {
        // Given
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((User)null);

        // When / Then
        await Assert.ThrowsAsync<NotFoundException>(() => _service.DemoteUser(1));
    }

    [Fact]
    public async Task Given_UserExists_When_SetUserActiveStatus_Then_UpdatesIsActive()
    {
        // Given
        var user = new User("user", new Email("a@a.com"), new Password("pass"), UserType.CommonUser);
        typeof(User).GetProperty("Id").SetValue(user, 1);
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(user);

        var dto = new SetUserActiveStatusDto { UserId = 1, IsActive = false };

        // When
        await _service.SetUserActiveStatus(dto);

        // Then
        Assert.False(user.IsActive);
        _userRepositoryMock.Verify(x => x.UpdateAsync(user), Times.Once);
    }

    [Fact]
    public async Task Given_UserDoesNotExist_When_SetUserActiveStatus_Then_ThrowsNotFoundException()
    {
        // Given
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((User)null);

        var dto = new SetUserActiveStatusDto { UserId = 1, IsActive = false };

        // When / Then
        await Assert.ThrowsAsync<NotFoundException>(() => _service.SetUserActiveStatus(dto));
    }

    [Theory]
    [InlineData("admin", UserType.Administrator)]
    [InlineData("user", UserType.CommonUser)]
    public void Given_RoleString_When_ParseUserType_Then_ReturnsCorrectUserType(string role, UserType expected)
    {
        // Given
        var method = typeof(UserAdminAppService).GetMethod("ParseUserType", BindingFlags.NonPublic | BindingFlags.Instance);

        // When
        var result = method.Invoke(_service, new object[] { role });

        // Then
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Given_InvalidRoleString_When_ParseUserType_Then_ThrowsArgumentException()
    {
        // Given
        var method = typeof(UserAdminAppService).GetMethod("ParseUserType", BindingFlags.NonPublic | BindingFlags.Instance);

        // When / Then
        Assert.Throws<TargetInvocationException>(() => method.Invoke(_service, new object[] { "invalid" }));
    }
}