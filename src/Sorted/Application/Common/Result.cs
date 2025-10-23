public class Result<T>
{
    public bool IsSuccess { get; set; }
    public T Value { get; set; }
    public List<string> Errors { get; set; }

    public static Result<T> Success(T success)
    {
        Result<T> successful = new Result<T>();
        successful.IsSuccess = true;
        successful.Value = success;
        return successful;
    }

    public static Result<T> Failure(List<string> errors)
    {
        Result<T> successful = new Result<T>();
        successful.IsSuccess = false;
        successful.Errors = errors;
        return successful;
    }

    public string ErrorsToString()
    {
        return String.Join(", ", Errors.ToArray());
    }
}