using System.IdentityModel.Tokens.Jwt;
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
        var tokenDescriptor = new SecurityTokenDescriptor(); // ira ter todas as info do token
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token); // converte o token para uma string
    }
}

// dotnet add package Microsoft.AspNetCore.Authentication
// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer  --> bearer - tipo de autenticacao do jwt
