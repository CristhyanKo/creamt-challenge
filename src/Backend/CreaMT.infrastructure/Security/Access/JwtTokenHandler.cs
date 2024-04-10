using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CreaMT.infrastructure.Security.Access;
public abstract class JwtTokenHandler
{
    protected static SymmetricSecurityKey SecurityKey(string signinkey)
    {
        var bytes = Encoding.UTF8.GetBytes(signinkey);
        return new SymmetricSecurityKey(bytes);
    }
}
