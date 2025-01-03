using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Domain.Abstractions
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }
        public bool IsError => !IsSuccess;
        public Error[] Errors { get; protected set; } = Array.Empty<Error>();

        protected Result(bool isSuccess, Error[] errors)
        {
            if (isSuccess && errors.Length > 0 || !isSuccess && errors.Length == 0)
            {
                throw new ArgumentException("Invalid error.", nameof(errors));
            }

            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Result Success() => new(true, Array.Empty<Error>());
        public static Result Failure(Error[] errors) => new(false, errors);
    }

    public class Result<T> : Result
    {
        public T? Value { get; private set; }

        private Result(bool isSuccess, Error[] errors, T value)
            : base(isSuccess, errors)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new(true, Array.Empty<Error>(), value);
        public static new Result<T> Failure(Error[] errors) => new(false, errors, default);
    }

}
