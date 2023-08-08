using EmployeesAPI2.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace EmployeesAPI2.Application.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly IAuthentificationService _authentificationService;

        public AuthorizationFilter(IAuthentificationService authentificationService)
        {
            _authentificationService = authentificationService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Obtener el token del header Authorization
            string authorizationHeader = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (authorizationHeader is not null && authorizationHeader.StartsWith("Bearer "))
            {
                // Removemos el prefijo Bearer
                string token = authorizationHeader.Substring("Bearer ".Length).Trim();

                try
                {
                    // Validamos el token y sacamos los contactos principales
                    ClaimsPrincipal principalClaims = _authentificationService.ValidateToken(token);

                    // guardamos los claims principales en el contexto por si llegaramos a necesitarlos
                    context.HttpContext.User = principalClaims;
                }
                catch
                {
                    // El token no es válido o ha expirado
                    context.Result = new UnauthorizedResult();
                }
            }
            else
            {
                // No hay token en los headers
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
