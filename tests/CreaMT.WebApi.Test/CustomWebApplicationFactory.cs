using CleaMT.CommonTestUtilities.Entities;
using CreaMT.infrastructure.DataAcess;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CreaMT.WebApi.Test;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{   private CreaMT.Domain.Entities.Usuario _usuario = default!;
    private string _password = string.Empty;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CreaMTAPIDbContext>));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<CreaMTAPIDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(provider);
                });
                using var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<CreaMTAPIDbContext>();

                dbContext.Database.EnsureCreated();
                startDatabase(dbContext);

            });

    }
    public string GetEmail() => _usuario.Email;
    public string GetPassword() => _password;
    public string GetName() => _usuario.Nome;
    public string GetCpfCnpj() => _usuario.CpfCnpj;
    public string GetPhone() => _usuario.Telefone;
    public Guid GetUserIdentifier() => _usuario.UsuarioIdentifier;
    private void startDatabase(CreaMTAPIDbContext dbContext)
    {
        ( _usuario, _password) = UsuarioBuilder.Build();
        dbContext.Usuarios.Add(_usuario);

        dbContext.SaveChanges();
    }
}
