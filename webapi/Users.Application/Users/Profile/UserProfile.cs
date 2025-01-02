using AutoMapper;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.UpdateUser;
using Application.Common.Models;
using Users.Domain.Users.Models;
using Application.Users.Queries;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
        CreateMap<UpdateUserRequest, UpdateUserCommand>();
        CreateMap<User, UserDto>();
    }
}
