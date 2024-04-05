using CreaMT.Domain.Entities;
using CreaMT.Domain.Repositories.Cliente;
using CreaMT.Domain.Repositories.Documento;

namespace CreaMT.infrastructure.DataAcess.Repositories;
public class DocumentoRepository : IDocumentoWriteOnlyRepository
{
    private readonly CreaMTAPIDbContext _dbContext;

    public DocumentoRepository(CreaMTAPIDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Documento documento)
    {
        await _dbContext.Documentos.AddAsync(documento);
    }
}
