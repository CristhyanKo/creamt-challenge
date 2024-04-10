using CreaMT.Domain.Entities;
using CreaMT.Domain.Repositories.SolicitacoesDocumentos;
using Microsoft.EntityFrameworkCore;

namespace CreaMT.infrastructure.DataAcess.Repositories;
public class SolicitacaoDocumentosRepository : ISolicitacaoDocumentosWriteOnlyRepository
{
    private readonly CreaMTAPIDbContext _dbContext;

    public SolicitacaoDocumentosRepository(CreaMTAPIDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(SolicitacaoDocumento solicitacaoDocumento)
    {
        await _dbContext.SolicitacoesDocumentos.AddAsync(solicitacaoDocumento);
    }
}
