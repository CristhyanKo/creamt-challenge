using CleaMT.CommonTestUtilities.Request;
using CreaMT.Application.UseCases.Usuario.Register;
using CreaMT.Exceptions;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace CreaMT.Validators.Test.Usuarios.Register;
public class RegisterUsuarioValidatorTest
{
    [Fact]
    public void Success_User_CPF()
    {
        var validator = new RegisterUsuarioValidator();

        var request = RequestRegisterUsuarioJsonBuilder.BuildUserCPF();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Success_User_CNPJ()
    {
        var validator = new RegisterUsuarioValidator();

        var request = RequestRegisterUsuarioJsonBuilder.BuildUserCNPJ();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Name_Empty()
    {
        var validator = new RegisterUsuarioValidator();

        var request = RequestRegisterUsuarioJsonBuilder.BuildUserCPF();
        request.Nome = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.NOME_EMPTY));
    }

    [Fact]
    public void Error_Email_Empty()
    {
        var validator = new RegisterUsuarioValidator();

        var request = RequestRegisterUsuarioJsonBuilder.BuildUserCPF();
        request.Email = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_EMPTY));
    }

    [Fact]
    public void Error_Email_Invalid()
    {
        var validator = new RegisterUsuarioValidator();

        var request = RequestRegisterUsuarioJsonBuilder.BuildUserCPF();
        request.Email = "email.com";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_INVALID));
    }


    [Fact]
    public void Error_Telefone_Empty()
    {
        var validator = new RegisterUsuarioValidator();

        var request = RequestRegisterUsuarioJsonBuilder.BuildUserCPF();
        request.Telefone = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.TELEPHONE_EMPTY));
    }

    [Fact]
    public void Error_Telefone_Invalid()
    {
        var validator = new RegisterUsuarioValidator();

        var request = RequestRegisterUsuarioJsonBuilder.BuildUserCPF();
        request.Telefone = "982231111";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.TELEPHONE_INVALID));
    }


    [Fact]
    public void Error_CpfCnpj_Empty()
    {
        var validator = new RegisterUsuarioValidator();

        var request = RequestRegisterUsuarioJsonBuilder.BuildUserCPF();
        request.CpfCnpj = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.CPF_CNPJ_EMPTY));
    }

    [Fact]
    public void Error_CpfCnpj_Invalid()
    {
        var validator = new RegisterUsuarioValidator();

        var request = RequestRegisterUsuarioJsonBuilder.BuildUserCPF();
        request.CpfCnpj = "036789462123";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.CPF_CNPJ_INVALID));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Error_Senha_Invalid(int passwordLength)
    {
        var validator = new RegisterUsuarioValidator();

        var request = RequestRegisterUsuarioJsonBuilder.BuildUserCPF(passwordLength);
       

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.PASSWORD_INVALID));
    }

   [Fact]
    public void Error_Senha_Empty()
    {
        var validator = new RegisterUsuarioValidator();

        var request = RequestRegisterUsuarioJsonBuilder.BuildUserCPF();
        request.Senha = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.PASSSWORD_EMPTY));
    }
}
