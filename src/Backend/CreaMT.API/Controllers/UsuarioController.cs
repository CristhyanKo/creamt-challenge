using CreaMT.Communication.Requests;
using CreaMT.Communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreaMT.API.Controllers;
[Route("[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUsuariosJson),StatusCodes.Status201Created)]
    public IActionResult Register(RequestRegisterUsuarioJson request)
    { 
        return Created();
    }
}
