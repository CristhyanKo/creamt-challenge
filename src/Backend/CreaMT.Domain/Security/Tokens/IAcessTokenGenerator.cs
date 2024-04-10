namespace CreaMT.Domain.Security.Tokens;
public interface IAcessTokenGenerator
{
    public string Generate(Guid userIdentifier);
}
