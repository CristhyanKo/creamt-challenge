using CreaMT.Communication.Requests;
using CreaMT.Communication.Responses;

namespace CreaMT.Application.UseCases.Usuario.Update;
public interface IUsuarioUpdateUseCase
{
    public Task Execute(RequestUpdateUsuarioJson request);
}
