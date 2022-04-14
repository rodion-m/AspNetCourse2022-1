namespace Lesson17.PasswordHashing;

public class RegistrationResponse : ResponseModel<User>
{
    public RegistrationResponse(bool succeeded, string message, User result) : base(succeeded, message, result)
    {
    }
}