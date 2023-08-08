using EmployeesAPI2.Application.Services.Interfaces;
using EmployeesAPI2.Infrastructure.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EmployeesAPI2.Application.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly SymmetricSecurityKey key;
        public AuthentificationService(IConfiguration configuration)
        {
            // Obtener la clave secreta
            string secretKey = configuration["JWT:Key"].ToString();
            key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
        }

        public string GenerateToken(User user)
        {
            // Definir los claims del token
            Claim[] claims = new Claim[]
            {
                new Claim("username", user.Email),
                new Claim("iat", DateTime.UtcNow.ToLongTimeString())
            };

            // Definir los parámetros del token
            SecurityTokenDescriptor tokenParams = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.Aes128CbcHmacSha256)
            };

            // Crear el token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenParams);

            // Obtener el token en formato string
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);
        }
    }
}
