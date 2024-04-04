namespace CreaMT.Domain.Security;
public interface IAcessTokenGenerator
{
    public string Generate(Guid userIdentifier);
}
