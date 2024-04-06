using CreaMT.Domain.Repositories;

namespace CreaMT.infrastructure.DataAcess;
public class UnitOfWork : IUnitOfWork
{
    private readonly CreaMTAPIDbContext _dbContext;

    public UnitOfWork(CreaMTAPIDbContext dbContext) => _dbContext = dbContext;

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
