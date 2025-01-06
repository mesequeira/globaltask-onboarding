using FluentValidation;
using FluentValidation.Results;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Users.Commands;
using Users.Application.Users.Dtos;
using Users.Application.Users.Validators;
using Users.Domain.Abstractions;
using Users.Domain.Abstractions.Interfaces;
using Users.Domain.Users.Abstractions;
using Users.Domain.Users.Errors;
using Users.Domain.Users.Models;

namespace Users.Application.Users.Handlers
{
    public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateUserCommand> _updateUserValidator;
        public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IValidator<UpdateUserCommand> updateUserValidator)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _updateUserValidator = updateUserValidator;
        }

        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _updateUserValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                Error[] errors = validationResult
                                    .Errors
                                    .Select(e => new Error(e.ErrorCode, e.ErrorMessage))
                                    .ToArray();

                return Result.Failure(errors, statusCode: 400);
            }

            User? user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                return Result.Failure([UserErrors.NotFound(request.Id)], statusCode: 404);
            }

            user.Update(request.Email, request.Password, request.PhoneNumber);

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
