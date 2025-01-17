// Application/Users/Commands/CreateUser/CreateUserCommandHandler.cs

using MediatR;
using Users.Application.Abstractions;
using Users.Application.Users.Events;
using Users.Domain.Abstractions;
using Users.Domain.Interfaces;
using Users.Domain.Models;

namespace Users.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : 
    IRequestHandler<CreateUserCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        
        var user = new User(
            request.Name,
            request.Email,
            request.PhoneNumber,
            request.BirthDate
        );

        await _userRepository.Create(user);

        // Guarda los cambios
        await _unitOfWork.SaveChangesAsync(); 
       
        await _eventBus.PublishAsync(new UserRegisteredEvent(user.Id, user.Name, user.Email));


        return Result<int>.Sucess(user.Id, 201);
    }
}