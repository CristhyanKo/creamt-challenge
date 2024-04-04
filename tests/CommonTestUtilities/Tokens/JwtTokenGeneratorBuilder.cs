using CreaMT.Domain.Security;
using CreaMT.infrastructure.Security.Access.Generator;

namespace CleaMT.CommonTestUtilities.Tokens;
public class JwtTokenGeneratorBuilder
{
    public static IAcessTokenGenerator Build() => new JwtTokenGenerator(expirationTimeMinute: 5, signinkey: "ffffffffffffffffffffffffffffffff");
}
