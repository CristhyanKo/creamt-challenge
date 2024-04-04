using CreaMT.Domain.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CreaMT.infrastructure.Security.Access.Generator;
public class JwtTokenGenerator : IAcessTokenGenerator
{
    private readonly uint _expirationTimeMinutes;
    private readonly string _signinkey;

    public JwtTokenGenerator(uint expirationTimeMinute, string signinkey)
    {
        _expirationTimeMinutes = expirationTimeMinute;
        _signinkey = signinkey;
    }
    public string Generate(Guid userIdentifier)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Sid, userIdentifier.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(_signinkey), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SecurityKey(string signinkey)
    {
        var bytes = Encoding.UTF8.GetBytes(signinkey);
        return new SymmetricSecurityKey(bytes);
    }
   
}