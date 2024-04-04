using CreaMT.Domain.Repositories.Usuario;
using Moq;

namespace CleaMT.CommonTestUtilities.Repositories;
public class UsuarioReadOnlyRepositoryBuilder
{   private readonly Mock<IUsuarioReadOnlyRepository> _repository;

    public UsuarioReadOnlyRepositoryBuilder() => _repository = new Mock<IUsuarioReadOnlyRepository>();
    public IUsuarioReadOnlyRepository Build()
    {
        return _repository.Object;
    }

    public void ExistActiveUsuarioWithEmail(string email)
    {
        _repository.Setup(repository => repository.ExistActiveUsuarioWithEmail(email)).ReturnsAsync(true);
    }

    public void ExistActiveUsuarioWithCpfCnpj(string CpfCnpj)
    {
        _repository.Setup(repository => repository.ExistActiveUsuarioWithCpfCnpj(CpfCnpj)).ReturnsAsync(true);
    }
}
