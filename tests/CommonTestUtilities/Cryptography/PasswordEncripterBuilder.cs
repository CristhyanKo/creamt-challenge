using CreaMT.Application.Services.Cryptography;

namespace CleaMT.CommonTestUtilities.Cryptography;
public class PasswordEncripterBuilder
{
    public static PasswordEncripter Build() => new PasswordEncripter("abc1234");
}
