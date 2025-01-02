// Application/Users/Queries/GetUserById/GetUserByIdQueryHandler.cs

using MediatR;
using Users.Domain.Interfaces;
using Users.Domain.Models;

namespace Users.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetById(request.Id);
    }
}