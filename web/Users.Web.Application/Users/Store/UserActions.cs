using Users.Web.Domain.DTO;
using Users.Web.Domain.Models;

namespace Users.Web.Application.Users.Store;

// public record LoadUsersAction;
public class GetUsersAction
{
}
public class FetchDataAction
{
}

public class FetchDataResultAction
{
    public List<UserDTO> Users { get; set; }
    
    public FetchDataResultAction(List<UserDTO> users)
    {
        Users = users;
    }
}
// public record LoadUsersSuccessAction(List<UserDTO> Users);
// public record LoadUsersFailureAction(string ErrorMessage);