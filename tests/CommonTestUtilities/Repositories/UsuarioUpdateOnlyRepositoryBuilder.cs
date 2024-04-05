using CreaMT.Domain.Entities;
using CreaMT.Domain.Repositories.Usuario;
using Moq;

namespace CleaMT.CommonTestUtilities.Repositories;
public class UsuarioUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IUsuarioUpdateOnlyRepository> _repository;

    public UsuarioUpdateOnlyRepositoryBuilder() => _repository = new Mock<IUsuarioUpdateOnlyRepository>();
    public UsuarioUpdateOnlyRepositoryBuilder GetById(Usuario usuario)
    {
        _repository.Setup(x => x.GetById(usuario.Id)).ReturnsAsync(usuario);
        return this;
    }

    public IUsuarioUpdateOnlyRepository Build() => _repository.Object;
}
