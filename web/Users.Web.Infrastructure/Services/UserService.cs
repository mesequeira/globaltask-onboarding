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
    
    public async Task<int?> PostUsersAsync(UserDTO newUser)
    {
        string url = "api/users";

        // Make the POST request
        var response = await _httpClient.PostAsJsonAsync(url, newUser);

        // Ensure a successful response
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        // Deserialize the response
        var result = await response.Content.ReadFromJsonAsync<ApiGenericResponse<int>>();

        // Return the user ID if the request was successful
        return result?.IsSuccess == true ? result.Value : null;
    }
    
    public async Task<bool> UpdateUserAsync(int id, UserDTO updatedUser)
    {
        string url = $"api/users/{id}";

        var response = await _httpClient.PatchAsJsonAsync(url, updatedUser);

        // Verificar si la respuesta fue exitosa
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        else return true;

        // // Deserializar la respuesta
        // var result = await response.Content.ReadFromJsonAsync<ApiGenericResponse<int>>();
        //
        // // Retornar true si la actualización fue exitosa
        // return result?.IsSuccess == true;
    }
    
    public async Task<bool> DeleteUserAsync(int id)
    {
        string url = $"api/users/{id}";

        var response = await _httpClient.DeleteAsync(url);

        // Retorna true si la respuesta es exitosa (204 No Content)
        return response.IsSuccessStatusCode;
    }

}