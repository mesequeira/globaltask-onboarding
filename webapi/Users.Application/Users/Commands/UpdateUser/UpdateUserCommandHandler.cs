// Application/Users/Commands/UpdateUser/UpdateUserCommandHandler.cs

using MediatR;
using Users.Domain.Interfaces;
using Users.Domain.Models;

namespace Users.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetById(request.Id);

        if (existingUser == null)
            return false;

        // Actualiza los campos
        // A menos que sean inmutables, en cuyo caso se haría distinto
        existingUser.GetType().GetProperty(nameof(existingUser.Name))?.SetValue(existingUser, request.Name);
        existingUser.GetType().GetProperty(nameof(existingUser.Email))?.SetValue(existingUser, request.Email);
        existingUser.GetType().GetProperty(nameof(existingUser.PhoneNumber))?.SetValue(existingUser, request.PhoneNumber);
        existingUser.GetType().GetProperty(nameof(existingUser.BirthDate))?.SetValue(existingUser, request.BirthDate);

        _userRepository.Update(existingUser);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}