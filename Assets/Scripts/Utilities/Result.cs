namespace Utilities
{
    public readonly struct Result<T>
    {
        public bool IsSuccessful { get; }
        public T Value { get; }
        Result(bool isSuccessful, T value)
        {
            IsSuccessful = isSuccessful;
            Value = value;
        }

        public static Result<T> Successful(T value) => new Result<T>(true,value);
        public static Result<T> Fail(T value) => new Result<T>(false,value);
    }
}
