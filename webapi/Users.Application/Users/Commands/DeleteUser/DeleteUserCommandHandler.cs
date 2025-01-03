// Application/Users/Commands/DeleteUser/DeleteUserCommandHandler.cs

using MediatR;
using Users.Application.Errors.User;
using Users.Domain.Abstractions;
using Users.Domain.Interfaces;

namespace Users.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<object?>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object?>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0) return Result<object?>.Failure(400, UserErrors.BadParameters);
        
        var existingUser = await _userRepository.GetById(request.Id);

        // if (existingUser == null)
        //     return Result<object>.Failure(404, UserErrors.NotFound);

        var deletion = await _userRepository.Delete(request.Id);
        
        if (!deletion) return Result<object?>.Failure(404, UserErrors.NotDeleted);
        
        await _unitOfWork.SaveChangesAsync();

        return Result<object?>.Sucess(null, 204);
    }
}