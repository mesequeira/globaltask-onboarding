using MediatR;
using Users.Domain.Abstractions;
using Users.Domain.Abstractions.Interfaces;
using Users.Domain.Users.Abstractions;
using Users.Domain.Users.Errors;
using Users.Domain.Users.Models;

namespace Users.Application.Users.Commands.Delete;

public sealed class DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand, Result>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user == null)
        {
            return Result.Failure([UserErrors.NotFound(request.Id)], statusCode: 404);
        }

        userRepository.Delete(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(statusCode: 200);
    }
}
