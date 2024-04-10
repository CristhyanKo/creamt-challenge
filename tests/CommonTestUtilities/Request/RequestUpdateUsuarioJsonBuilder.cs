using Bogus;
using Bogus.Extensions.Brazil;
using CreaMT.Communication.Requests;
using CreaMT.Domain.Extensions;

namespace CleaMT.CommonTestUtilities.Request;
public class RequestUpdateUsuarioJsonBuilder
{
    public static RequestUpdateUsuarioJson Build()
    {
        return new Faker<RequestUpdateUsuarioJson>()
        .RuleFor(x => x.Nome, f => f.Person.FullName)
        .RuleFor(x => x.Email, f => f.Internet.Email())
        .RuleFor(x => x.CpfCnpj, f => f.Person.Cpf().RemoveMascara())
        .RuleFor(x => x.Telefone, f => f.Phone.PhoneNumber("###########"))
        .Generate();
    }
}
