using CreaMT.Domain.Repositories.Usuario;
using Moq;

namespace CleaMT.CommonTestUtilities.Repositories;
public class UsuarioWriteOnlyRepositoryBuilder
{
    public static IUsuarioWriteOnlyRepository Build()
    {
        var mock = new Mock<IUsuarioWriteOnlyRepository>();
        return mock.Object;
    }
}
