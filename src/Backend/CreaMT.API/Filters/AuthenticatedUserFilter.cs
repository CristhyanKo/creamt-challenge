using CreaMT.Communication.Responses;
using CreaMT.Domain.Repositories.Usuario;
using CreaMT.Domain.Security.Tokens;
using CreaMT.Domain.Extensions;
using CreaMT.Exceptions.ExceptionsBase;
using CreaMT.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace CreaMT.API.Filters;

public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator _accessTokenValidator;
    private readonly IUsuarioReadOnlyRepository _usuarioReadOnlyRepository;
    public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator, IUsuarioReadOnlyRepository usuarioReadOnlyRepository)
    {
        _accessTokenValidator = accessTokenValidator;
        _usuarioReadOnlyRepository = usuarioReadOnlyRepository;
    }
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);
            var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);
            var exist = await _usuarioReadOnlyRepository.ExistActiveUsuarioWithIdentifier(userIdentifier);

            if (exist.IsFalse())
            {
                throw new CreaMTException(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
            }
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessagesException.TOKEN_EXPIRED)
            {
                TokenExpired = true,
            });
        }
        catch (CreaMTException ex)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
        }
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();
        if (string.IsNullOrEmpty(authentication))
        {
            throw new CreaMTException(ResourceMessagesException.NO_TOKEN);
        }

        return authentication["Bearer ".Length..].Trim();

    }
}
