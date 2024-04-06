using CreaMT.Domain.Entities;
using CreaMT.Domain.Repositories.Solicitacao;

namespace CreaMT.infrastructure.DataAcess.Repositories;
public class SolicitacaoRepository : ISolicitacaoWriteOnlyRepository
{
    private readonly CreaMTAPIDbContext _dbContext;

    public SolicitacaoRepository(CreaMTAPIDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Solicitacao solicitacao)
    {
        throw new NotImplementedException();
    }
}
