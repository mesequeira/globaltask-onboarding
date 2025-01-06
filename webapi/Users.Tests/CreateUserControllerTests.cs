using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Users.Application.Users.Commands.CreateUser;
using Users.Domain.Abstractions;
using Users.WebApi.Controllers.User;

namespace Users.Tests
{
    public class CreateUserControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly UsersController _controller;

        public CreateUserControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new UsersController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction_WhenResultIsSuccess()
        {
            // ---------- ARRANGE ----------
            var command = new CreateUserCommand(
                Name: "John",
                Email: "john@example.com",
                PhoneNumber: "1234567",
                BirthDate: new DateTime(1990, 1, 1)
            );

            // Simulamos un resultado exitoso
            var successResult = Result<int>.Sucess(42, 201);

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(successResult);

            // ---------- ACT ----------
            var actionResult = await _controller.Create(command);

            // ---------- ASSERT ----------
            // Verificamos que es un CreatedAtActionResult
            var createdAtAction = actionResult as CreatedAtActionResult;
            createdAtAction.Should().NotBeNull("debe retornar CreatedAtActionResult en caso de éxito");

            // Verificamos el nombre de la acción a la que se hace referencia (GetById)
            createdAtAction!.ActionName.Should().Be("GetById");

            // Verificamos el valor devuelto (el objeto anónimo de CreateResponseObject)
            createdAtAction.Value.Should().BeEquivalentTo(new
            {
                value = 42,
                isSuccess = true,
                statusCode = 201,
                error = new
                {
                    ErrorCode = "",
                    Message = ""
                }
            }, 
            options => options.WithStrictOrdering()); 
            // .WithStrictOrdering() es opcional si quieres forzar el mismo orden de propiedades

            // Si deseas, también puedes comprobar RouteValues (por ejemplo, el ID):
            createdAtAction.RouteValues.Should().ContainKey("id").WhoseValue.Should().Be(42);

            // Verificamos que el mediador fue invocado
            _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenResultIsFailure()
        {
            // ---------- ARRANGE ----------
            var command = new CreateUserCommand(
                Name: "Invalid",
                Email: "invalid-email",
                PhoneNumber: "12",
                BirthDate: DateTime.Now
            );
            // var command = new CreateUserCommand(
            //     Name: "Invalid",
            //     Email: "invalid@email.com",
            //     PhoneNumber: "12123456",
            //     BirthDate: new DateTime(2000, 1,1)
            // );

            // Simulamos un resultado fallido
            var failureResult = Result<int>.Failure(400, new Error("0001", "Fallo al crear usuario"));

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(failureResult);

            // ---------- ACT ----------
            var actionResult = await _controller.Create(command);

            // ---------- ASSERT ----------
            var badRequestResult = actionResult as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull("debe retornar BadRequestResult en caso de fallo");

            badRequestResult!.Value.Should().BeEquivalentTo(new
            {
                isSuccess = false,
                statusCode = 400,
                ErrorCode = "0001",
                Message = "Fallo al crear usuario"
            },
            options => options.WithStrictOrdering());

            _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
