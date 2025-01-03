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
using Users.Domain.Abstractions;
using Users.Domain.Abstractions.Interfaces;
using Users.Domain.Users.Abstractions;
using Users.Domain.Users.Models;

namespace Users.Application.Users.Handlers
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateUserCommand> _createUserValidator;

        public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IValidator<CreateUserCommand> createUserValidator)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _createUserValidator = createUserValidator;
        }

        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _createUserValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                Error[] errors = validationResult
                                    .Errors
                                    .Select(e => new Error(e.ErrorCode, e.ErrorMessage))
                                    .ToArray();
                
                return Result<Guid>.Failure(errors);
            }

            User user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                Birthday = request.Birthday,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber
            };

            _userRepository.Insert(user);
            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Success(user.Id);
        }
    }
}
