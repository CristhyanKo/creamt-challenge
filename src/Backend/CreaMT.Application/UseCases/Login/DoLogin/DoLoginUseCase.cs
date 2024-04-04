using CreaMT.Application.Services.Cryptography;
using CreaMT.Communication.Requests;
using CreaMT.Communication.Responses;
using CreaMT.Domain.Repositories.Usuario;
using CreaMT.Domain.Security;
using CreaMT.Exceptions.ExceptionsBase;

namespace CreaMT.Application.UseCases.Login.DoLogin;
  
public class DoLoginUseCase :IDoLoginUseCase
{
    private readonly IUsuarioReadOnlyRepository _repository;
    private readonly IAcessTokenGenerator _accessTokenService;
    private readonly PasswordEncripter _passwordEncripter;

    public DoLoginUseCase(IUsuarioReadOnlyRepository repository, 
        PasswordEncripter passwordEncripter,
        IAcessTokenGenerator accessTokenService)
    {
        _repository = repository;
        _passwordEncripter = passwordEncripter;
        _accessTokenService = accessTokenService;
    }

    public async Task<ResponseRegisteredUsuariosJson> Execute(RequestLoginJson request)
    {
        var encriptedPassword = _passwordEncripter.Encrypt(request.Senha);

        var usuario = await _repository.GetByEmailAndPassword(request.Email, encriptedPassword) ?? throw new InvalidLoginException();

        return new ResponseRegisteredUsuariosJson
        {
            Nome = usuario.Nome,
            Tokens = new ResponseTokenJson
            {
                AccessToken = _accessTokenService.Generate(usuario.UsuarioIdentifier)
            }
        };

    }
}
