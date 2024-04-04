namespace CreaMT.Communication.Responses;
public class ResponseRegisteredUsuariosJson
{
    public string Nome { get; set; } = string.Empty;
    public ResponseTokenJson Tokens { get; set; } = default!;
}
