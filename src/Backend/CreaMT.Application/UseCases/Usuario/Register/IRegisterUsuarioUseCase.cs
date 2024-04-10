using CreaMT.Communication.Requests;
using CreaMT.Communication.Responses;

namespace CreaMT.Application.UseCases.Usuario.Register;
public interface IRegisterUsuarioUseCase
{
    public Task<ResponseRegisteredUsuariosJson> Execute(RequestRegisterUsuarioJson request);
}
