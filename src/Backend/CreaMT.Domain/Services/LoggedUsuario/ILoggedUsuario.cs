using CreaMT.Domain.Entities;

namespace CreaMT.Domain.Services.LoggerUser;
public interface ILoggedUsuario
{
    public Task<Usuario> Usuario();
}
