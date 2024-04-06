using CreaMT.Communication.Requests;

namespace CreaMT.Application.UseCases.Usuario.ChangePassword;
public interface IChangePasswordUseCase
{
    public Task Execute(RequestChangePasswordJson request);
}
