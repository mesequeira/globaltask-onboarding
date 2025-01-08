namespace Users.Worker.Domain.Abstractions;

public class Result
{
    public int StatusCode { get; set; }
    public bool IsSuccess { get; protected set; }
    public bool IsError => !IsSuccess;
    public Error[] Errors { get; protected set; } = Array.Empty<Error>();

    protected Result(bool isSuccess, Error[] errors, int statusCode)
    {
        if (isSuccess && errors.Length > 0 || !isSuccess && errors.Length == 0)
        {
            throw new ArgumentException("Invalid error.", nameof(errors));
        }

        StatusCode = statusCode;
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result Success(int statusCode = 200) => new(true, Array.Empty<Error>(), statusCode);
    public static Result Failure(Error[] errors, int statusCode = 400) => new(false, errors, statusCode);
}

public class Result<T> : Result
{
    public T? Value { get; private set; }

    private Result(bool isSuccess, Error[] errors, int statusCode,T value)
        : base(isSuccess, errors, statusCode)
    {
        Value = value;
    }

    public static Result<T> Success(T value, int statusCode = 200) => new(true, Array.Empty<Error>(), statusCode,value);
    public static new Result<T> Failure(Error[] errors, int statusCode = 400) => new(false, errors, statusCode, default);
}
