using Bogus;
using Bogus.Extensions.Brazil;
using CleaMT.CommonTestUtilities.Cryptography;
using CreaMT.Domain.Entities;
using CreaMT.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaMT.CommonTestUtilities.Entities;
public class UsuarioBuilder
{
    public static (Usuario usuario, string password) Build()
    { 
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var password = new Faker().Internet.Password();

        var usuario = new Faker<Usuario>()
            .RuleFor(x => x.Id, () => 1)
            .RuleFor(x => x.Nome, f => f.Person.FirstName)
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Senha, f => passwordEncripter.Encrypt(password))
            .RuleFor(x => x.CpfCnpj, f => f.Person.Cpf().RemoveMascara())
            .RuleFor(x => x.Telefone, f => f.Phone.PhoneNumber("###########"));

        return (usuario, password);
    }
}
