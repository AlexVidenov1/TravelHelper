using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TravelHelper.Api.Models;

namespace TravelHelper.Api.Utils;
public static class JwtGenerator
{
    public static string Generate(User u, IConfiguration cfg)
    {
        var claims = new[]
        {
new Claim(JwtRegisteredClaimNames.Sub,u.Id.ToString()),
new Claim(JwtRegisteredClaimNames.Email,u.Email)
};
        var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(cfg["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
        issuer: cfg["Jwt:Issuer"],
        claims: claims,
        expires: DateTime.UtcNow.AddHours(6),
        signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}