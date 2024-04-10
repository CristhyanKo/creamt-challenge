using CreaMT.Communication.Requests;
using CreaMT.Communication.Responses;

namespace CreaMT.Application.UseCases.Login.DoLogin;
public interface IDoLoginUseCase
{
    public Task<ResponseRegisteredUsuariosJson> Execute(RequestLoginJson request);
}
