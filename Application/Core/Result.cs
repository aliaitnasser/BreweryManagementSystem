namespace Application.Core
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        
        public static Result<T> Success(T value, string message) =>  new() { IsSuccess = true, Value = value, Message = message};
        public static Result<T> Failure(string error) => new() { IsSuccess = false, Error = error};
    }
}