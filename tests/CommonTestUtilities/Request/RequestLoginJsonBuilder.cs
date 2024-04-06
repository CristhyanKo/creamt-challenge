using CreaMT.Communication.Requests;
using System.Reflection.Metadata.Ecma335;
using Bogus;
namespace CleaMT.CommonTestUtilities.Request;
public class RequestLoginJsonBuilder
{
    public static RequestLoginJson Build()
    {
        return new Faker<RequestLoginJson>()
        .RuleFor(x => x.Email, f => f.Internet.Email())
        .RuleFor(x => x.Senha, f => f.Internet.Password());
    }
}
