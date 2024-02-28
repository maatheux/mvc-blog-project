using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Models;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Services;

public class TokenService
{
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler(); // manipulador do token
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey); // a chave tem q ser passada como uma array de bytes
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new (ClaimTypes.Name, "andrebaltieri"), // acesso pelo User.Identity.Name
                new (ClaimTypes.Role, "admin"), // User.IsInRole()
                new (ClaimTypes.Role, "user"),
                new ("fruta", "banana")
            }), // attr para setar os claims (afirmacaoes sobre o token / objetos chave-valor). E o ASP.NET ja possui alguns tipos de claim
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials
            (
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature
            ) // como token sera gerado/lido/(des)encriptografado
            
        }; // ira ter todas as info do token
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token); // converte o token para uma string
    }
}

// dotnet add package Microsoft.AspNetCore.Authentication
// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer  --> bearer - tipo de autenticacao do jwt
