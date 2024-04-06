using CreaMT.Communication.Responses;

namespace CreaMT.Application.UseCases.Profile;
public interface IGetUsuarioProfileUseCase
{
    public Task<ResponseUserProfileJson> Execute();
}
