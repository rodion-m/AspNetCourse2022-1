namespace Lesson17.PasswordHashing;

public class ResponseModel<TResultObject>
{
    public ResponseModel(bool succeeded, string message, TResultObject result)
    {
        Succeeded = succeeded;
        Message = message;
        Result = result;
    }

    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public TResultObject Result { get; set; }
}

public class ErrorModel
{
    public string? Message { get; set; }
}