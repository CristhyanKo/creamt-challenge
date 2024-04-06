namespace CreaMT.Domain.Entities;
public class Cliente : EntityBase
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CpfCnpj { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public bool AnuidadePaga { get; set; } = false;
    public long UsuarioId { get; set; }
}
