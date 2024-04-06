using AutoMapper;
using CreaMT.Communication.Responses;
using CreaMT.Domain.Services.LoggerUser;

namespace CreaMT.Application.UseCases.Profile;
public class GetUsuarioProfileUseCase : IGetUsuarioProfileUseCase
{
    private readonly ILoggedUsuario _loggedUsuario;
    private readonly IMapper _mapper;
    public GetUsuarioProfileUseCase(ILoggedUsuario loggedUsuario, IMapper mapper)
    {
        _loggedUsuario = loggedUsuario;
        _mapper = mapper;
    }
    public async Task<ResponseUserProfileJson> Execute()
    {
       var use = await _loggedUsuario.Usuario();
        return _mapper.Map<ResponseUserProfileJson>(use);
    }
}
