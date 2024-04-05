using CreaMT.Domain.Entities;
using CreaMT.Domain.Security.Tokens;
using CreaMT.Domain.Services.LoggerUser;
using CreaMT.infrastructure.DataAcess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CreaMT.infrastructure.Services.LoggedUsuario;
public class LoggedUsuario : ILoggedUsuario
{
    private readonly CreaMTAPIDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUsuario(CreaMTAPIDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<Usuario> Usuario()
    {
        var token = _tokenProvider.Value();
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        var usuarioIdentifir = Guid.Parse(identifier);
        return await _dbContext
            .Usuarios
            .AsNoTracking()
            .FirstAsync(usuario => usuario.Ativo && usuario.UsuarioIdentifier == usuarioIdentifir); 
    }
}
