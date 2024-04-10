using CreaMT.Domain.Entities;
using CreaMT.Domain.Repositories.Cliente;
using CreaMT.Domain.Repositories.Documento;
using Microsoft.EntityFrameworkCore;

namespace CreaMT.infrastructure.DataAcess.Repositories;
public class DocumentoRepository : IDocumentoWriteOnlyRepository, IDocumentoReadOnlyRepository, IDocumentoUpdateOnlyRepository
{
    private readonly CreaMTAPIDbContext _dbContext;

    public DocumentoRepository(CreaMTAPIDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Documento documento)
    {
        await _dbContext.Documentos.AddAsync(documento);
    }

    public async Task<IList<Documento>> GetAll()
    {
        return await _dbContext
            .Documentos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IList<Documento>> GetAllFromService(long servicoId)
    {
        return await _dbContext
            .Documentos
            .Where(servico => servico.ServicoId == servicoId)
            .ToListAsync();
    }
}
