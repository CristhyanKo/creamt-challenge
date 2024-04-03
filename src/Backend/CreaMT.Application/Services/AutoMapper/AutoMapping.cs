using AutoMapper;
using CreaMT.Communication.Requests;

namespace CreaMT.Application.Services.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
    }
    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUsuarioJson, Domain.Entities.Usuario>()
            .ForMember(dest => dest.Senha, opt => opt.Ignore());
    }

   
}
