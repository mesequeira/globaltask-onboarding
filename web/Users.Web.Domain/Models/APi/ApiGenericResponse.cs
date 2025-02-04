namespace Users.Web.Domain.Models.APi;

public class ApiGenericResponse<T>
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public ApiError? Error { get; set; }
    public T Value { get; set; } = default!;
}

public class ApiError
{
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}