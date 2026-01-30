namespace LoanApp.Domain.Common;

public sealed class Result<T>
{
    private Result(T value) { IsSuccess = true; Value = value; }
    private Result(IDictionary<string, string[]>? validation, IList<Error>? errors)
    { 
        IsSuccess = false;
        ValidationErrors = validation;
        Errors = errors; 
    }

    public bool IsSuccess { get; }
    public T? Value { get; }
    public IDictionary<string, string[]>? ValidationErrors { get; }
    public IList<Error>? Errors { get; }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> ValidationFailure(IDictionary<string, string[]> errors) => new(errors, null);
    public static Result<T> Failure(params Error[] errors) => new(null, errors);
    //public static Result<T> Failure(Error error) => new(false, error);

    public static implicit operator Result<T>(T value) => Success(value);
}
