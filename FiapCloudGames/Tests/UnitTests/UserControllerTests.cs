using Moq;
using FiapCloudGamesApi.Controllers;
using Application.Interfaces;
using Application.DTOs.User.Signatures;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGamesApi.Tests.UnitTests.UserControllerTests
{ 
    public class UserControllerTests
    {
        private readonly UserController _controller;
        private readonly Mock<IUserAppService> _userAppServiceMock;

        public UserControllerTests()
        {
            _userAppServiceMock = new Mock<IUserAppService>();
            _controller = new UserController(_userAppServiceMock.Object);
        }

        [Fact]
        public async Task Register_ReturnsNoContent()
        {
            var dto = new RegisterDto
            {
                Email = "teste@email.com",
                Password = "senha123",
                UserName = "usuarioTeste"
            };

            _userAppServiceMock
                .Setup(s => s.Register(It.Is<RegisterDto>(d =>
                    d.Email == dto.Email &&
                    d.Password == dto.Password &&
                    d.UserName == dto.UserName)))
                .Returns(Task.CompletedTask);

            var result = await _controller.Register(dto);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task ChangePassword_ReturnsNoContent()
        {
            var dto = new ChangePasswordDto
            {
                Password = "senhaAntiga",
                NewPassword = "senhaNova"
            };

            _userAppServiceMock
                .Setup(s => s.ChangePassword(It.Is<ChangePasswordDto>(d =>
                    d.Password == dto.Password &&
                    d.NewPassword == dto.NewPassword)))
                .Returns(Task.CompletedTask);

            var result = await _controller.ChangePassword(dto);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNoContent()
        {
            var dto = new DeleteUserDto
            {
                Password = "senha123"
            };

            _userAppServiceMock.Setup(s => s.DeleteUser(dto)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteUser(dto);

            Assert.IsType<NoContentResult>(result);
        }
    }
}