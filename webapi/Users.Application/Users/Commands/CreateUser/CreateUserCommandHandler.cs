using MediatR;
using Application.Common.Interfaces;
using Application.Common.Models;
using Users.Domain.Users.Models;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return Result<int>.Failure(
                code: "BadRequest",
                description: "La solicitud no puede ser nula.",
                type: "Validation",
                statusCode: 400
            );
        }

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Birthday = request.Birthday,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(user.Id, 201, "Usuario creado correctamente.");
    }
}
