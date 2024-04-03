using CreaMT.Application.Services.AutoMapper;
using CreaMT.Application.Services.Cryptography;
using CreaMT.Application.UseCases.Usuario.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace CreaMT.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddPasswordsEncrypter(services, configuration);
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
    }

    private static void AddPasswordsEncrypter(IServiceCollection services, IConfiguration configuration)
    {
        var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");
        services.AddScoped(option => new PasswordEncripter(additionalKey!));
    }
}
