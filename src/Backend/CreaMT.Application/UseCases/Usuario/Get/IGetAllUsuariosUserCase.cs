using CreaMT.Communication.Requests;
using CreaMT.Communication.Responses;

namespace CreaMT.Application.UseCases.Usuario.Get;
public interface IGetAllUsuariosUserCase
{
    public Task<ResponseListUserJson> Execute();
}
