using CreaMT.Domain.Entities;
using CreaMT.Domain.Services.LoggerUser;
using Moq;

namespace CleaMT.CommonTestUtilities;
public class LoggedUsuarioBuilder
{
    public static ILoggedUsuario Build(Usuario usuario)
    {
        var mock = new Mock<ILoggedUsuario>();

        mock.Setup(x => x.Usuario()).ReturnsAsync(usuario);

        return mock.Object; 
    }
}
