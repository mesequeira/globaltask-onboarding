// Application/Users/Commands/UpdateUser/UpdateUserCommandHandler.cs

using MediatR;
using Users.Application.Abstractions;
using Users.Application.Errors.User;
using Users.Domain.Abstractions;
using Users.Domain.Interfaces;
using Users.Domain.Models;
using Users.Worker.Application.Users.Events;
using Users.Worker.Domain.Abstractions;

namespace Users.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task<Result<int>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0) return Result<int>.Failure(400, UserErrors.BadParameters);
        
        var existingUser = await _userRepository.GetById(request.Id);

        if (existingUser == null)
            return Result<int>.Failure(404, UserErrors.NotFound);

        // Actualiza los campos
        // A menos que sean inmutables, en cuyo caso se haría distinto
        existingUser.Name = request.Name;
        existingUser.Email = request.Email;
        existingUser.PhoneNumber = request.PhoneNumber;
        existingUser.BirthDate = request.BirthDate;
        existingUser.ModifiedAt = DateTime.Now;
        
        await _userRepository.Update(existingUser);
        
        await _unitOfWork.SaveChangesAsync();
        
        await _eventBus.PublishAsync(new UserUpdatedEvent(request.Email, new Dictionary<string, FieldChange>()));

        return Result<int>.Sucess(existingUser.Id, 204);
    }
}