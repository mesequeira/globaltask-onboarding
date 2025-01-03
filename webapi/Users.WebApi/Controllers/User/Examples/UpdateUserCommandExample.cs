using Swashbuckle.AspNetCore.Filters;
using Users.Application.DTOs.Users;
using Users.Application.Users.Commands.UpdateUser;

namespace Users.WebApi.Controllers.User.Examples;

public class UpdateUserCommandExample : IExamplesProvider<UpdateUserCommandDTO>
{
    public UpdateUserCommandDTO GetExamples()
    {
        return new UpdateUserCommandDTO(
            "Lautaro Vargas",
            "Lauti@example.com",
            "22356945",
            new DateTime(2001, 5, 25)
        );
    }
}