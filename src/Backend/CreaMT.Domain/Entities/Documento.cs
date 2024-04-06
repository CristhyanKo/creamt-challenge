namespace CreaMT.Domain.Entities;
public class Documento : EntityBase
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string EnderecoArquivo { get; set; } = string.Empty;
    public long ServicoId { get; set; }
    public long UsuarioId { get; set; }
}
