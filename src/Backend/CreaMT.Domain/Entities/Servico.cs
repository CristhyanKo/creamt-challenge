namespace CreaMT.Domain.Entities;
public class Servico : EntityBase
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public long UsuarioId { get; set; }
    public ICollection<Documento> Documentos { get; set; }
}