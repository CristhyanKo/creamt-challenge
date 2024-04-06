using CleaMT.CommonTestUtilities.Request;
using CreaMT.Application.UseCases.Usuario.Register;
using CreaMT.Application.UseCases.Usuario.Update;
using CreaMT.Exceptions;
using FluentAssertions;
using Xunit;

namespace CreaMT.Validators.Test.Usuarios.Update;
public class UpdateUsuarioValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new UpdateUsuarioValidator();
        var request = RequestUpdateUsuarioJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }


    [Fact]
    public void Error_Name_Empty()
    {
        var validator = new UpdateUsuarioValidator();
        var request = RequestUpdateUsuarioJsonBuilder.Build();
        request.Nome = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.NOME_EMPTY));
    }

    [Fact]
    public void Error_Email_Empty()
    {
        var validator = new UpdateUsuarioValidator();
        var request = RequestUpdateUsuarioJsonBuilder.Build();
        request.Email = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_EMPTY));
    }

    [Fact]
    public void Error_Email_Invalid()
    {
        var validator = new UpdateUsuarioValidator();
        var request = RequestUpdateUsuarioJsonBuilder.Build();
        request.Email = "email.com";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_INVALID));
    }


    [Fact]
    public void Error_Telefone_Empty()
    {
        var validator = new UpdateUsuarioValidator();
        var request = RequestUpdateUsuarioJsonBuilder.Build();
        request.Telefone = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.TELEPHONE_EMPTY));
    }

    [Fact]
    public void Error_Telefone_Invalid()
    {
        var validator = new UpdateUsuarioValidator();
        var request = RequestUpdateUsuarioJsonBuilder.Build();
        request.Telefone = "982231111";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.TELEPHONE_INVALID));
    }


    [Fact]
    public void Error_CpfCnpj_Empty()
    {
        var validator = new UpdateUsuarioValidator();
        var request = RequestUpdateUsuarioJsonBuilder.Build();
        request.CpfCnpj = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.CPF_CNPJ_EMPTY));
    }

    [Fact]
    public void Error_CpfCnpj_Invalid()
    {
        var validator = new UpdateUsuarioValidator();
        var request = RequestUpdateUsuarioJsonBuilder.Build();
        request.CpfCnpj = "036789462123";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.CPF_CNPJ_INVALID));
    }
}
