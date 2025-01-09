﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Users.Application.Abstractions;
using Users.Application.Common.Services.Interfaces;
using Users.Application.Users.Events;
using Users.Domain.Abstractions;
using Users.Domain.Abstractions.Interfaces;
using Users.Domain.Users.Abstractions;
using Users.Domain.Users.Errors;
using Users.Domain.Users.Models;

namespace Users.Application.Users.Commands.Update;

public sealed class UpdateUserCommandHandler(
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork, 
        IValidator<UpdateUserCommand> updateUserValidator,
        IEventBus eventBus,
        IPropertyChangeTracker<User, UpdateUserCommand> propertyChangeTracker
    ) : IRequestHandler<UpdateUserCommand, Result>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await updateUserValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            Error[] errors = validationResult
                                .Errors
                                .Select(e => new Error(e.ErrorCode, e.ErrorMessage))
                                .ToArray();

            return Result.Failure(errors, statusCode: 400);
        }

        User? user = await userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user == null)
        {
            return Result.Failure([UserErrors.NotFound(request.Id)], statusCode: 404);
        }

        Dictionary<string, FieldChange> updatedProps = propertyChangeTracker.GetChanges(user, request);

        user.Update(request.Email, request.Password, request.PhoneNumber);

        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await eventBus.PublishAsync(new UserUpdatedEvent(request.Email, updatedProps));

        return Result.Success();
    }
}
