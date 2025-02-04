using System.Net.Http;
using System.Net.Http.Json;
using Users.Web.Domain.DTO;
using Users.Web.Domain.Models;
using Users.Web.Domain.Models.APi;
using Users.Web.Domain.Services;

namespace Users.Web.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UserDTO>?> GetUsersAsync(int page = 1, int size = 25, string sortBy = "name")
    {
        // Construimos la URL con los parámetros de consulta
        string url = $"api/users?page={page}&size={size}&sortBy={sortBy}";

        // Obtener la respuesta y mapear correctamente
        var response = await _httpClient.GetFromJsonAsync<ApiGenericResponse<PaginatedUsersResponse>>(url);

        // Si la respuesta es exitosa, devolver la lista de usuarios
        return response?.IsSuccess == true ? response.Value.Users : null;
        
    }
}