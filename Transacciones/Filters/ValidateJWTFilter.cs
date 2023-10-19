using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Transacciones.Negocio.DTO;
using Transacciones.Negocio.Services.Interfaces;

namespace Transacciones.Filters
{
    public class ValidateJWTFilter : IActionFilter
    {
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;
        public ValidateJWTFilter(IJwtService jwtService, IConfiguration configuration)
        {
            _jwtService = jwtService;
            _configuration = configuration;
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
        

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.GetTokenAsync("access_token").Result;
            JWT_Values jWT_Values = _configuration.GetSection("Jwt").Get<JWT_Values>();
            if (!_jwtService.validateToken(token,jWT_Values))
            {
                var unAuthorizedResult = new UnauthorizedObjectResult("Acceso denegado, su token ha vencido");
                context.Result = unAuthorizedResult;
            }
        }
    }
}
