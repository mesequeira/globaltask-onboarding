using Swashbuckle.AspNetCore.Filters;
using Users.Application.Users.Commands.CreateUser;

public class CreateUserCommandExample : IExamplesProvider<CreateUserCommand>
{
    public CreateUserCommand GetExamples()
    {
        return new CreateUserCommand(
            "Juan Pérez",
            "juan.perez@example.com",
            "5491112345678",
            new DateTime(1990, 5, 15)
        );
    }
}