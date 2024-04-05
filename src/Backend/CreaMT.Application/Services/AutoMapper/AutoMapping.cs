using AutoMapper;
using CreaMT.Communication.Requests;
using CreaMT.Communication.Responses;

namespace CreaMT.Application.Services.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DomainToResponse();
    }
    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUsuarioJson, Domain.Entities.Usuario>()
            .ForMember(dest => dest.Senha, opt => opt.Ignore());
    }

    private void DomainToResponse()
    {
        CreateMap<Domain.Entities.Usuario, ResponseUserProfileJson>();
    }


}
