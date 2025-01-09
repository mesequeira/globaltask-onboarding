using MediatR;
using Users.Application.Abstractions;
using Users.Application.Users.Events;
using Users.Domain.Abstractions;
using Users.Domain.Abstractions.Interfaces;
using Users.Domain.Users.Abstractions;
using Users.Domain.Users.Errors;
using Users.Domain.Users.Models;

namespace Users.Application.Users.Commands.Delete;

public sealed class DeleteUserCommandHandler(
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork,
        IEventBus eventBus
    ) : IRequestHandler<DeleteUserCommand, Result>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user == null)
        {
            return Result.Failure([UserErrors.NotFound(request.Id)], statusCode: 404);
        }

        string userName = user.UserName;
        string email = user.Email;

        userRepository.Delete(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await eventBus.PublishAsync(new UserDeletedEvent(
                 userName,
                 email,
                 request.Reason
              ));

        return Result.Success(statusCode: 200);
    }
}
