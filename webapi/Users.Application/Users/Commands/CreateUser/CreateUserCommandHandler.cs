// Application/Users/Commands/CreateUser/CreateUserCommandHandler.cs

using MediatR;
using Users.Domain.Abstractions;
using Users.Domain.Interfaces;
using Users.Domain.Models;

namespace Users.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Validar Usuario
        if (false)
        {
            return Result<int>.Failure(new int(), 400, new Error("0001", "User No se pudo crear el usuario"));
        }
        
        var user = new User(
            request.Name,
            request.Email,
            request.PhoneNumber,
            request.BirthDate
        );

        await _userRepository.Create(user);

        // Guarda los cambios
        await _unitOfWork.SaveChangesAsync(); 
        // o si no hay UnitOfWork
        // await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Sucess(user.Id, 201);
    }
}