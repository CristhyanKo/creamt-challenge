using AutoMapper;
using CreaMT.Communication.Responses;
using CreaMT.Domain.Repositories.Usuario;
using CreaMT.Domain.Services.LoggerUser;

namespace CreaMT.Application.UseCases.Usuario.Get;
public class GetAllUsuariosUserCase : IGetAllUsuariosUserCase
{   private readonly IUsuarioReadOnlyRepository _readOnlyRepository;
    private readonly IMapper _mapper;
    public GetAllUsuariosUserCase(IUsuarioReadOnlyRepository readOnlyRepository,
        ILoggedUsuario loggedUsuario,
        IMapper mapper)
    {
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
    }
    public async Task<ResponseListUserJson> Execute()
    {
      var usuarios = await _readOnlyRepository.GetAll();
        
      return new ResponseListUserJson
      {
          Usuarios = _mapper.Map<List<ResponseUserJson>> (usuarios)
      };
    }
}
