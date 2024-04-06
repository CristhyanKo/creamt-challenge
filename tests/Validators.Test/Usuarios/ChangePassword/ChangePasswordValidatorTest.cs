using CleaMT.CommonTestUtilities.Request;
using CreaMT.Application.UseCases.Usuario.ChangePassword;
using CreaMT.Exceptions;
using FluentAssertions;
using Xunit;

namespace CreaMT.Validators.Test.Usuarios.ChangePassword;
public class ChangePasswordValidatorTest
{

    [Fact]
    public void success()
    {

        var validator = new ChangePasswordValidator();
        var request = RequestChangePasswordJsonBuilder.Build();

        var result = validator.Validate(request);
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Error_Senha_Invalid(int passwordLength)
    {
        var validator = new ChangePasswordValidator();

        var request = RequestChangePasswordJsonBuilder.Build(passwordLength);


        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.PASSWORD_INVALID));
    }

    [Fact]
    public void Error_Senha_Empty()
    {
        var validator = new ChangePasswordValidator();

        var request = RequestChangePasswordJsonBuilder.Build();
        request.NovaSenha = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.PASSSWORD_EMPTY));
    }
}
