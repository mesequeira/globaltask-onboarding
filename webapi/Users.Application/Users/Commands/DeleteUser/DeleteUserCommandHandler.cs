// Application/Users/Commands/DeleteUser/DeleteUserCommandHandler.cs

using MediatR;
using Users.Domain.Interfaces;

namespace Users.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetById(request.Id);

        if (existingUser == null)
            return false;

        var deletion = _userRepository.Delete(request.Id);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}