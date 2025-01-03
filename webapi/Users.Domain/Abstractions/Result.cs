namespace Users.Domain.Abstractions;

public sealed record Error(string Code, string? Description = null)
{
    public static readonly Error None = new (string.Empty);
}

public class Result<T>
{
    
    private readonly T _value;
    public bool IsSuccess { get; }

    public int StatusCode { get; }

    // public bool IsFailure => !IsSuccess;
    public Error Error { get;}
    
    private Result(T value,bool isSuccess, int statusCode, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid Error", nameof(error));
        }

        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Error = error;
        _value = value;
    }
    public T Value
    {
        get
        {
            if (!IsSuccess)
                throw new InvalidOperationException("Cannot access the value of a failed result.");
            return _value;
        }
    }
    

    public static Result<T> Sucess(T value, int code) => new(value, true, code,Error.None);
    public static Result<T> Failure(T value, int code, Error error) => new(value, false, code, error);
    
    
    public object CreateResponseObject()
    {
        if (IsSuccess)
            return new
            {
                value = Value,
                isSuccess = IsSuccess,
                statusCode = StatusCode,
                error = new
                {
                    ErrorCode = Error.Code,
                    Message = Error.Description,
                }
            };
        
        return new
        {
            isSuccess = IsSuccess,
            statusCode = StatusCode,
            ErrorCode = Error.Code,
            Message   = Error.Description
        }; 
    }

}