using Users.Domain.Abstractions;

namespace Users.Application.Errors.User;

public static class UserErrors
{
    // Reglas de Negocio
    public static readonly Error NotFound = new Error("User.NotFound", "User Not Found");
    public static readonly Error AlreadyExists = new Error("User.Duplicated", "User already exists");
    public static readonly Error NotDeleted = new Error("User.NotDeleted", "Can not delete user");
    
    // Reglas de negocio
    public static readonly Error BadParameters = new Error("User.BadParameters", "Wrong Parameters Sent");

}