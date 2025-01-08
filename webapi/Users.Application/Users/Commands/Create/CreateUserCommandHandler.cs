using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Users.Application.Abstractions;
using Users.Domain.Abstractions;
using Users.Domain.Abstractions.Interfaces;
using Users.Domain.Users.Abstractions;
using Users.Domain.Users.Events;
using Users.Domain.Users.Models;

namespace Users.Application.Users.Commands.Create;

public sealed class CreateUserCommandHandler(
    IUserRepository userRepository, 
    IUnitOfWork unitOfWork, 
    IValidator<CreateUserCommand> createUserValidator,
    IEventBus eventBus 
    ) : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await createUserValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            Error[] errors = validationResult
                                .Errors
                                .Select(e => new Error(e.ErrorCode, e.ErrorMessage))
                                .ToArray();

            return Result<Guid>.Failure(errors, statusCode: 400);
        }

        User user = User.Create(
            request.Email,
            request.Password,
            request.PhoneNumber,
            request.UserName,
            request.Birthday
        );

        userRepository.Insert(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await eventBus.PublishAsync(new UserRegisteredEvent(user.Id, user.UserName, user.Email));

        return Result<Guid>.Success(user.Id, statusCode: 201);
    }
}
