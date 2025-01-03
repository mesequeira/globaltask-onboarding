// Application/Users/Queries/GetUserById/GetUserByIdQueryHandler.cs

using MediatR;
using Users.Application.Errors.User;
using Users.Domain.Abstractions;
using Users.Domain.Interfaces;
using Users.Domain.Models;

namespace Users.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<User>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id < 0) return Result<User>.Failure(400, UserErrors.BadParameters);
        var user = await _userRepository.GetById(request.Id);

        return user != null ? Result<User>.Sucess(user, 200) : Result<User>.Failure(404, UserErrors.NotFound);
    }
}