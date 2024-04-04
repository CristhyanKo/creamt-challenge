using Bogus;
using Bogus.Bson;
using Bogus.DataSets;
using Bogus.Extensions;
using Bogus.Extensions.Brazil;
using CreaMT.Communication.Requests;
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
        .RuleFor(x => x.CpfCnpj, f => RemoveMascara(f.Person.Cpf()))
        .RuleFor(x => x.Telefone, f => f.Phone.PhoneNumber("###########"))
        .Generate();
    }
    public static RequestRegisterUsuarioJson BuildUserCNPJ()
    {
        return new Faker<RequestRegisterUsuarioJson>()
            .RuleFor(x => x.Nome, f => f.Company.CompanyName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Senha, f => f.Internet.Password())
            .RuleFor(x => x.CpfCnpj, f => RemoveMascara(f.Company.Cnpj()))
            .RuleFor(x => x.Telefone, f => f.Phone.PhoneNumber("###########"))
            .Generate();
    }

    private static string RemoveMascara(string value)
    {
        // Remove todos os caracteres não numéricos do CNPJ
        return Regex.Replace(value, "[^0-9]", "");
    }
}
