namespace Dominos.Core.OperationResults;

public class OperationResult<TEntity>
{
    public bool IsSuccess { get; }
    public TEntity Value { get; }
    public string Error { get; }

    private OperationResult(bool isSuccess, TEntity value, string error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static OperationResult<TEntity> Success(TEntity value) =>
        new OperationResult<TEntity>(true, value, string.Empty);

    public static OperationResult<TEntity> Fail(string error) =>
        new OperationResult<TEntity>(false, default, error);
}
