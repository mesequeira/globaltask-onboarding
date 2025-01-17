// Application/Users/Commands/DeleteUser/DeleteUserCommandHandler.cs

using MediatR;
using Users.Application.Abstractions;
using Users.Application.Errors.User;
using Users.Application.Users.Events;
using Users.Domain.Abstractions;
using Users.Domain.Interfaces;
using Users.Worker.Application.Users.Events;
using Users.Worker.Domain.Abstractions;

namespace Users.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<object?>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;

    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task<Result<object?>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0) return Result<object?>.Failure(400, UserErrors.BadParameters);
        
        var existingUser = await _userRepository.GetById(request.Id);

        var deletion = await _userRepository.Delete(request.Id);
        
        if (!deletion) return Result<object?>.Failure(404, UserErrors.NotDeleted);
        
        await _unitOfWork.SaveChangesAsync();
        
        await _eventBus.PublishAsync(new UserDeletedEvent(
            existingUser.Name,
            existingUser.Email,
            "request.Reason"
        ));

        return Result<object?>.Sucess(null, 204);
    }
}