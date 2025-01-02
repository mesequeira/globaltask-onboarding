// Application/Users/Commands/CreateUser/CreateUserCommandHandler.cs

using MediatR;
using Users.Domain.Interfaces;
using Users.Domain.Models;

namespace Users.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(
            request.Id,
            request.Name,
            request.Email,
            request.PhoneNumber,
            request.BirthDate
        );

        _userRepository.Create(user);

        // Guarda los cambios
        await _unitOfWork.SaveChangesAsync(); 
        // o si no hay UnitOfWork
        // await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}