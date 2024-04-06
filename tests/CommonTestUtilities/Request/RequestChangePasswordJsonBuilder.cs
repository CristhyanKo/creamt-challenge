using Bogus;
using CreaMT.Communication.Requests;

namespace CleaMT.CommonTestUtilities.Request;
public class RequestChangePasswordJsonBuilder
{
    public static RequestChangePasswordJson Build(int passwordLength = 10 )
    {
        return new Faker<RequestChangePasswordJson>()
        .RuleFor(x => x.Senha, f => f.Internet.Password())
        .RuleFor(x => x.NovaSenha, f => f.Internet.Password(passwordLength));
    }
}
