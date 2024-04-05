namespace CreaMT.Domain.Entities;
public class SolicitacaoDocumento : EntityBase
{
    public long SolicitacaoId { get; set; }
    public long DocumentoId { get; set; }
    public long UsuarioId { get; set; }
    public int Downloads { get; set; }
    public Documento Documento { get; set; }
}
