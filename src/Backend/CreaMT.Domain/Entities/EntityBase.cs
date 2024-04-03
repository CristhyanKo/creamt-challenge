namespace CreaMT.Domain.Entities;
public class EntityBase
{
    public long Id { get; set; }
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    public DateTime? DataAtualizacao { get; set; } = null;
    public bool Ativo { get; set; } = true;
    public bool Excluido { get; set; } = false;
}
