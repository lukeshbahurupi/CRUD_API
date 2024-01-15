using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public static class JwtTokenUtility
{
    private const string SecretKey = "your-secret-key";
    private static readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
    private static readonly SigningCredentials SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

    public static string GenerateToken(string username, bool isAdmin)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User"),
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1), // Token expiration time
            signingCredentials: SigningCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
