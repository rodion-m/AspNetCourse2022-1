using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Lesson14.Models;
using Microsoft.IdentityModel.Tokens;

namespace Lesson19.Auth.Services;

public class JwtTokenService : ITokenService
{
    private readonly JwtConfig _jwtConfig;

    public JwtTokenService(JwtConfig jwtConfig)
    {
        _jwtConfig = jwtConfig;
    }

    public string GenerateToken(Account account)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                //new Claim("id", account.Id.ToString()),
                new Claim(ClaimTypes.Role, string.Join(",", account.Roles))
            }),
            Expires = DateTime.UtcNow.Add(_jwtConfig.LifeTime),
            Audience = _jwtConfig.Audience,
            Issuer = _jwtConfig.Issuer,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_jwtConfig.SigningKeyBytes),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var tokenHandler = new JwtSecurityTokenHandler(); // {MapInboundClaims = false};
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}