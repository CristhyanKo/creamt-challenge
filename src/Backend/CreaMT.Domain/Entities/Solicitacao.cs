namespace CreaMT.Domain.Entities;
public class Solicitacao : EntityBase
{
    public long ServicoId { get; set; }
    public long ClienteId { get; set; }
    public long UsuarioId { get; set; }
    public bool Pago { get; set; }

    public ICollection<SolicitacaoDocumento> SolicitacaoDocumentos { get; set; }
}
