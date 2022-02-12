namespace Services.Utilities.Result

{

    public interface IResult
    {
        bool IsSuccess { get; }
        string? Message { get; }
    }
}
