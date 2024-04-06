namespace CreaMT.Domain.Repositories;
public interface IUnitOfWork
{
    public Task Commit();
}
