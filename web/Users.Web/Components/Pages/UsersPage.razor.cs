using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text.Json.Serialization;
using Users.Web.Domain.Abstractions;
using Users.Web.Domain.Users;
using Users.Web.Domain.Users.Dtos;
using Users.Web.Infrastructure.Refit.Users;

namespace Users.Web.Manegement.Components.Pages;

public partial class UsersPage
{
    [Inject]
    public required IUsersApi _usersApi { get; set; }
    public List<UserResponseDto> UserList { get; set; } = new();

    protected override async void OnInitialized()
    {
        base.OnInitialized();

        //Result<PaginatedUserDto> result = await _usersApi.GetUsers(null, null, null);

        //if (result.IsSuccess)
        //{
        //    UserList = result.Value!.Data;
        //}

        var httpClient = new HttpClient();

        HttpResponseMessage response = await httpClient.GetAsync("http://users.webapi:8080/api/Users?page=1&size=25&sortBy=UserName");

        if (response.IsSuccessStatusCode)
        {
            string responseContent = await response.Content.ReadAsStringAsync();

            Result<PaginatedUserDto> result = JsonSerializer.Deserialize<Result<PaginatedUserDto>>(responseContent)!;

            UserList = result.Value!.Data;
        }

       

    }
}