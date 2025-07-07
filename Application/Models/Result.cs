namespace Application.Models
{
    public class Result<T>
    {
        public T? Value { get; }
        public string? ErrorMessage { get; }
        public bool IsSuccess => ErrorMessage == null;
        public bool IsFailed => ErrorMessage != null;

        private Result(T value)
        {
            Value = value;
            ErrorMessage = null;
        }
        private Result(string errorMessage)
        {
            Value = default;
            ErrorMessage = errorMessage;
        }
        public static Result<T> Success(T value) => new(value);

        public static Result<T> Failed(string errorMessage) => new(errorMessage);
    }

    public class Result
    {
        public string? ErrorMessage { get; }
        public bool IsSuccess => ErrorMessage == null;
        public bool IsFailed => ErrorMessage != null;


        private Result()
        {
            ErrorMessage = null;
        }

        private Result(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        public static Result Success() => new Result();

        public static Result Failed(string errorMessage) => new Result(errorMessage);
    }
}
