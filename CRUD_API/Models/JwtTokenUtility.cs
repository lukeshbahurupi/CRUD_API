using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

/*public static class JwtTokenUtility
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
}*/
public static class JwtTokenUtility
{
    private const int SecretKeyLength = 32;

    private static readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(GenerateRandomBytes(SecretKeyLength));
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

    private static byte[] GenerateRandomBytes(int length)
    {
        using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
        {
            byte[] randomBytes = new byte[length];
            rng.GetBytes(randomBytes);
            return randomBytes;
        }
    }
}

