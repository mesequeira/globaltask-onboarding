// Application/Users/Queries/GetAllUsers/GetAllUsersQueryHandler.cs

using MediatR;
using Users.Domain.Interfaces;
using Users.Domain.Models;

namespace Users.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _userRepository.Get();
        return users;
    }
}