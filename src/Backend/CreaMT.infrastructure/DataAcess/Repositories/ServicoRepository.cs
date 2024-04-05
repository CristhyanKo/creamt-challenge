using CreaMT.Domain.Entities;
using CreaMT.Domain.Repositories.Servico;

namespace CreaMT.infrastructure.DataAcess.Repositories;
public class ServicoRepository : IServicoWriteOnlyRepository
{
    private readonly CreaMTAPIDbContext _dbContext;

    public ServicoRepository(CreaMTAPIDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Servico servico)
    {
        await _dbContext.Servicos.AddAsync(servico);
    }
}
