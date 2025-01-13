namespace Users.Web.Domain.Abstractions;

public class Result
{
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public bool IsError => !IsSuccess;
    public Error[] Errors { get; protected set; } = Array.Empty<Error>();
}

public class Result<T> : Result
{
    public T? Value { get; set; }
}
