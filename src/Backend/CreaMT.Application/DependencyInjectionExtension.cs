using CreaMT.Application.Services.AutoMapper;
using CreaMT.Application.UseCases.Login.DoLogin;
using CreaMT.Application.UseCases.Profile;
using CreaMT.Application.UseCases.Usuario.ChangePassword;
using CreaMT.Application.UseCases.Usuario.Delete;
using CreaMT.Application.UseCases.Usuario.Get;
using CreaMT.Application.UseCases.Usuario.Register;
using CreaMT.Application.UseCases.Usuario.Update;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace CreaMT.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddScoped(option => new AutoMapper.MapperConfiguration(option =>
        {
            option.AddProfile(new AutoMapping());
        }).CreateMapper());
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUsuarioUseCase, RegisterUsuarioUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetUsuarioProfileUseCase, GetUsuarioProfileUseCase>();
        services.AddScoped<IUsuarioUpdateUseCase, UpdateUsuarioUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        services.AddScoped<IGetAllUsuariosUserCase, GetAllUsuariosUserCase>();
        services.AddScoped<IDeleteUsuarioUseCase, DeleteUsuarioUseCase>();
    }
}
