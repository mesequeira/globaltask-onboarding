using MediatR;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;

    public DeleteUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);

        if (user == null)
        {
            return Result<Unit>.Failure(
                code: "NotFound",
                description: $"Usuario con ID {request.Id} no encontrado.",
                type: "Validation",
                statusCode: 404
            );
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Unit>.Success(Unit.Value, 200, "Usuario eliminado correctamente.");
    }
}
