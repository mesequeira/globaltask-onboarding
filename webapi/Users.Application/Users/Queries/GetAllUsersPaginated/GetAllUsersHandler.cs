// Application/Users/Queries/GetAllUsers/GetAllUsersQueryHandler.cs

using System.Linq.Expressions;
using MediatR;
using Users.Application.DTOs.Users;
using Users.Domain.Abstractions;
using Users.Domain.Interfaces;
using Users.Domain.Models;

namespace Users.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<UsersPaginatedListDTO>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    private static readonly Dictionary<string, Expression<Func<User, object>>> SortExpressions = new(StringComparer.OrdinalIgnoreCase)
    {
        { "name",        u => u.Name },
        { "email",       u => u.Email },
        { "phonenumber", u => u.PhoneNumber },
        { "birthdate",   u => u.BirthDate }
        // Puedes agregar más mapeos aquí...
    };

    public async Task<Result<UsersPaginatedListDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        // Si no encontramos la expresión en el diccionario, usamos una por defecto (Id).
        if (!SortExpressions.TryGetValue(request.sortBy.ToLower(), out var sortExpression))
        {
            sortExpression = u => u.Id; 
        }

        if (request.size < 0 || request.page < 0)
        {
            return Result<UsersPaginatedListDTO>.Failure(new UsersPaginatedListDTO(), 400, new Error("0001", "Invalid pagination values"));
        }
        
        var usersQuery = await _userRepository.GetAllAsQueryable();
        
        var totalCount = usersQuery.Count();
        
        
        

        // Aplicamos la expresión de ordenación
        usersQuery = usersQuery.OrderBy(sortExpression);

        // Calculamos el "skip" y aplicamos paginación
        var skip = (request.page - 1) * request.size;
        usersQuery = usersQuery.Skip(skip).Take(request.size);

        return Result<UsersPaginatedListDTO>.Sucess(
            new UsersPaginatedListDTO(
                totalCount,
                request.page,
                request.size,
                usersQuery
                ), 200);
    }
}