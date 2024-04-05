using CreaMT.API.Filters;
using CreaMT.API.Middleware;
using CreaMT.Application;
using CreaMT.infrastructure;
using CreaMT.infrastructure.Migrations;
using CreaMT.infrastructure.Extension;
using CreaMT.API.Converters;
using Microsoft.OpenApi.Models;
using CreaMT.Domain.Security.Tokens;
using CreaMT.API.Token;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new StringConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Cabe�alho de autoriza��o JWT usando o esquema Bearer.
                        Digite 'Bearer' [espa�o] e depois seu token na entrada de texto abaixo
                        Exemplo: 'Bearer 1234abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDatabase();
app.Run();

void MigrateDatabase()
{
    if (builder.Configuration.IsUnitTestEnviroment())
        return;
    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    DatabaseMigration.Migrat(builder.Configuration.GetConexaoCompleta(), serviceScope.ServiceProvider);
}

public partial class Program
{
    protected Program() { }
}
