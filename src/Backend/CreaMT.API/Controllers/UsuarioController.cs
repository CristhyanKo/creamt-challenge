using CreaMT.API.Attributes;
using CreaMT.Application.UseCases.Profile;
using CreaMT.Application.UseCases.Usuario.ChangePassword;
using CreaMT.Application.UseCases.Usuario.Register;
using CreaMT.Application.UseCases.Usuario.Update;
using CreaMT.Communication.Requests;
using CreaMT.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CreaMT.API.Controllers;
public class UsuarioController : CreaMTBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUsuariosJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUsuarioUseCase useCase,
        [FromBody] RequestRegisterUsuarioJson request)
    {
        var resul = await useCase.Execute(request);
        return Created(string.Empty, resul);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
    [AuthenticatedUser]
    public async Task<IActionResult> GetUserProfile(
    [FromServices] IGetUsuarioProfileUseCase useCase)
    {
        var resul = await useCase.Execute();
        return Ok(resul);
    }

    [HttpPut]
    [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task<IActionResult> update(
    [FromServices] IUsuarioUpdateUseCase useCase,
    [FromBody] RequestUpdateUsuarioJson request)
    {
        await useCase.Execute(request);
        return NoContent();
    }

    [HttpPut("change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task<IActionResult> ChangePassword(
  [FromServices] IChangePasswordUseCase useCase,
  [FromBody] RequestChangePasswordJson request)
    {
        await useCase.Execute(request);
        return NoContent();
    }
}
