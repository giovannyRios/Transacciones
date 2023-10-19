using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Negocio.DTO;
using Transacciones.Negocio.Services.Interfaces;

namespace Transacciones.Negocio.Services.Implements
{
    public class JwtService : IJwtService
    {
        public string generateToken(JWT_Values jWT_Values, Dictionary<string, string> customValues)
        {

            var tokenHandler = new JwtSecurityTokenHandler();

            //Add claims
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier,customValues.GetValueOrDefault("Usuario")));

            TimeZoneInfo timeZone = TimeZoneInfo.Local;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWT_Values.Key));
            var credencials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var jwtToken = new SecurityTokenDescriptor {
                    Subject = claims,
                    Expires = TimeZoneInfo.ConvertTime(DateTime.Now.AddMinutes(double.Parse(jWT_Values.ExpireTokenInMinutes)),timeZone),
                    NotBefore = TimeZoneInfo.ConvertTime(DateTime.Now, timeZone),
                    SigningCredentials = credencials,
                    Audience = jWT_Values.Audience,
                    Issuer = jWT_Values.Issuer
            };

            var tokenCreate = tokenHandler.CreateToken(jwtToken);
            return tokenHandler.WriteToken(tokenCreate);
        }

        public bool validateToken(string token, JWT_Values jWT_Values)
        {
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jWT_Values.Issuer,
                ValidAudience = jWT_Values.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWT_Values.Key))
            };

            ClaimsPrincipal claimPrincipal = new JwtSecurityTokenHandler().ValidateToken(token, parameters, out SecurityToken validatedToken);
            var tiempo = validatedToken.ValidFrom;
            var tiempo2 = validatedToken.ValidTo;
            return claimPrincipal.Identity.IsAuthenticated && validatedToken.ValidTo > DateTime.UtcNow;
        }
    }
}
