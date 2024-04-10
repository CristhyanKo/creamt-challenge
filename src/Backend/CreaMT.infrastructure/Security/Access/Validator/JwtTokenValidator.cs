using CreaMT.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CreaMT.infrastructure.Security.Access.Validator;
public class JwtTokenValidator : JwtTokenHandler, IAccessTokenValidator
{
    private readonly string _signinKey;
    public JwtTokenValidator(string signinKey) =>  _signinKey = signinKey;
    
    public Guid ValidateAndGetUserIdentifier(string token)
    {
        var ValidationParameter = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = SecurityKey(_signinKey),
            ClockSkew = new TimeSpan(0)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, ValidationParameter, out _);
        var userIdentifier = principal.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;
        return Guid.Parse(userIdentifier);
    }
}
