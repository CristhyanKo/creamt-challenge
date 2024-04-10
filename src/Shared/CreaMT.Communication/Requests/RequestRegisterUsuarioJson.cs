namespace CreaMT.Communication.Requests;
public class RequestRegisterUsuarioJson
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CpfCnpj { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;

}
