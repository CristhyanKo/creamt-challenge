namespace CreaMT.Exceptions.ExceptionsBase;
public class InvalidLoginException : CreaMTException
{
    public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
    {
    }
}
