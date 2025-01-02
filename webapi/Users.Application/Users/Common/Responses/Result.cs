namespace Application.Common.Models
{
    public class Result<T>
    {
        public T? Value { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ErrorDetails? Error { get; set; }

        public static Result<T> Success(T value, int statusCode = 200, string? message = null)
        {
            return new Result<T>
            {
                Value = value,
                IsSuccess = true,
                StatusCode = statusCode,
                Message = message,
                Error = null
            };
        }

        public static Result<T> Failure(string code, string description, string type, int statusCode = 400)
        {
            return new Result<T>
            {
                Value = default,
                IsSuccess = false,
                StatusCode = statusCode,
                Message = null,
                Error = new ErrorDetails
                {
                    Code = code,
                    Description = description,
                    Type = type
                }
            };
        }
    }

    public class ErrorDetails
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
