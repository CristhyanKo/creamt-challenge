namespace CreaMT.Domain.Entities;
public class Usuario : EntityBase
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string CpfCnpj { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public Guid UsuarioIdentifier { get; set; }
   
}
