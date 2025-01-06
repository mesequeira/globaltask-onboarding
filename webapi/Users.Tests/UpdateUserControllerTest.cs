using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Users.Application.Users.Commands.UpdateUser;
using Users.Domain.Abstractions;
using Users.WebApi.Controllers.User;
using Xunit;

namespace Users.Tests
{
    public class UpdateUsersControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly UsersController _controller;

        public UpdateUsersControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new UsersController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Update_ShouldReturnNoContent_WhenResultIsSuccess()
        {
            // ---------- ARRANGE ----------
            const int userId = 1;
            var command = new UpdateUserCommand(
                Id: 0,  // El controller sobrescribirá este valor con 'userId'
                Name: "John Updated",
                Email: "john.updated@example.com",
                PhoneNumber: "9999999",
                BirthDate: new DateTime(1995, 5, 5)
            );

            // Simulamos un resultado exitoso que retorna 204
            var successResult = Result<int>.Sucess(userId, 204);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(successResult);

            // ---------- ACT ----------
            // Llamamos al método Update con id = userId
            var actionResult = await _controller.Update(userId, command);

            // ---------- ASSERT ----------
            // Verificamos que es un NoContentResult
            var noContentResult = actionResult as NoContentResult;
            noContentResult.Should().NotBeNull("debe retornar NoContent cuando la actualización es exitosa");

            // Verificamos que el mediador fue llamado una sola vez 
            // con un UpdateUserCommand cuyo Id sea 1
            _mediatorMock.Verify(m => m.Send(It.Is<UpdateUserCommand>(c => c.Id == userId), 
                                             It.IsAny<CancellationToken>()),
                                 Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenResultIs404()
        {
            // ---------- ARRANGE ----------
            const int userId = 10;
            var command = new UpdateUserCommand(
                Id: 0, 
                Name: "Unknown",
                Email: "unknown@example.com",
                PhoneNumber: "9999999",
                BirthDate: DateTime.Now
            );

            // Simulamos un resultado fallido con 404
            var notFoundResult = Result<int>.Failure(404, new Error("USER_NOT_FOUND", "No se encontró el usuario."));
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(notFoundResult);

            // ---------- ACT ----------
            var actionResult = await _controller.Update(userId, command);

            // ---------- ASSERT ----------
            var notFoundObjectResult = actionResult as NotFoundObjectResult;
            notFoundObjectResult.Should().NotBeNull("debe retornar NotFound cuando el handler retorna 404");

            // El controller retorna result.CreateResponseObject(), un objeto anónimo
            // Podríamos verificarlo con BeEquivalentTo(...) según la forma:
            notFoundObjectResult!.Value.Should().BeEquivalentTo(new
            {
                isSuccess = false,
                statusCode = 404,
                ErrorCode = "USER_NOT_FOUND",
                Message = "No se encontró el usuario."
            });

            _mediatorMock.Verify(m => m.Send(It.Is<UpdateUserCommand>(c => c.Id == userId), 
                                             It.IsAny<CancellationToken>()),
                                 Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenIdsDontMatch()
        {
            // 1. Levantas la app en memoria con WebApplicationFactory
            using var factory = new WebApplicationFactory<Program>();
            using var client = factory.CreateClient();

            // 2. Creas el payload con ID=99
            var payload = new
            {
                id = 99,
                name = "Mismatch",
                email = "mismatch@example.com",
                phoneNumber = "111222333",
                birthDate = DateTime.Now.ToString("yyyy-MM-dd")
            };

            // 3. Envías la petición
            var response = await client.PatchAsJsonAsync("/api/users/5", payload);

            // 4. Verificas la respuesta
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("El ID del usuario no coincide con la ruta.");
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenHandlerReturnsOtherStatus()
        {
            // ---------- ARRANGE ----------
            const int userId = 2;
            var command = new UpdateUserCommand(
                Id: 0,
                Name: "Any Name",
                Email: "any@example.com",
                PhoneNumber: "55555",
                BirthDate: new DateTime(1990, 1, 1)
            );

            // Simulamos un Failure 400 distinto
            var failureResult = Result<int>.Failure(400, new Error("ANY_ERROR", "Algo salió mal."));

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(failureResult);

            // ---------- ACT ----------
            var actionResult = await _controller.Update(userId, command);

            // ---------- ASSERT ----------
            var badRequestObjectResult = actionResult as BadRequestObjectResult;
            badRequestObjectResult.Should().NotBeNull("debe retornar BadRequest cuando el handler retorna 400");

            badRequestObjectResult!.Value.Should().BeEquivalentTo(new
            {
                isSuccess = false,
                statusCode = 400,
                ErrorCode = "ANY_ERROR",
                Message = "Algo salió mal."
            });

            _mediatorMock.Verify(m => m.Send(It.Is<UpdateUserCommand>(c => c.Id == userId), 
                                             It.IsAny<CancellationToken>()),
                                 Times.Once);
        }
    }
}
