using CreaMT.API.Attributes;
using CreaMT.Application.UseCases.Login.DoLogin;
using CreaMT.Communication.Requests;
using CreaMT.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CreaMT.API.Controllers;

public class LoginController : CreaMTBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUsuariosJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseRegisteredUsuariosJson), StatusCodes.Status401Unauthorized)]

    public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson request)
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }
}
