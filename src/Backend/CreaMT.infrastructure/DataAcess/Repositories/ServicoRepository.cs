using CreaMT.Domain.Entities;
using CreaMT.Domain.Repositories.Servico;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CreaMT.infrastructure.DataAcess.Repositories;
public class ServicoRepository : IServicoWriteOnlyRepository, IServicoReadOnlyRepository
{
    private readonly CreaMTAPIDbContext _dbContext;

    public ServicoRepository(CreaMTAPIDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Servico servico)
    {
        await _dbContext.Servicos.AddAsync(servico);
    }

    public async Task<IList<Servico>> GetAll()
    {
        return await _dbContext
      .Servicos
      .AsNoTracking()
      .Include(d => d.Documentos)
      .ToListAsync();
    }
}
