using CreaMT.Domain.Security.Cryptography;
using CreaMT.infrastructure.Security.Cryptography;

namespace CleaMT.CommonTestUtilities.Cryptography;
public class PasswordEncripterBuilder
{
    public static IPasswordEncripter Build() => new Sha512Encripter("12s3dv");
}
