using Bogus;
using Bogus.Extensions.Brazil;
using CreaMT.Communication.Requests;
using CreaMT.Domain.Extensions;
using System.Text.RegularExpressions;

namespace CleaMT.CommonTestUtilities.Request;
public class RequestRegisterUsuarioJsonBuilder
{
    public static RequestRegisterUsuarioJson BuildUserCPF(int passwordLength = 10 )
    {

        return new Faker<RequestRegisterUsuarioJson>()
        .RuleFor(x => x.Nome, f => f.Person.FullName)
        .RuleFor(x => x.Email, f => f.Internet.Email())
        .RuleFor(x => x.Senha, f => f.Internet.Password(passwordLength))
        .RuleFor(x => x.CpfCnpj, f => f.Person.Cpf().RemoveMascara())
        .RuleFor(x => x.Telefone, f => f.Phone.PhoneNumber("###########"))
        .Generate();
    }
    public static RequestRegisterUsuarioJson BuildUserCNPJ()
    {
        return new Faker<RequestRegisterUsuarioJson>()
            .RuleFor(x => x.Nome, f => f.Company.CompanyName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Senha, f => f.Internet.Password())
            .RuleFor(x => x.CpfCnpj, f => f.Company.Cnpj().RemoveMascara())
            .RuleFor(x => x.Telefone, f => f.Phone.PhoneNumber("###########"))
            .Generate();
    }
}
