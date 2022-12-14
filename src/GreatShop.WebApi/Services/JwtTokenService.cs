using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GreatShop.Configurations;
using GreatShop.Domain.Entities;
using GreatShop.Domain.Services;
using Microsoft.IdentityModel.Tokens;

namespace GreatShop.WebApi.Services;

public class JwtTokenService : ITokenService
{
    private readonly JwtConfig _jwtConfig;
    private readonly IClock _clock;

    public JwtTokenService(JwtConfig jwtConfig, IClock clock)
    {
        _jwtConfig = jwtConfig ?? throw new ArgumentNullException(nameof(jwtConfig));
        _clock = clock ?? throw new ArgumentNullException(nameof(clock));
    }

    public string GenerateToken(Account account)
    {
        if (account == null) throw new ArgumentNullException(nameof(account));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Role, string.Join(",", account.Roles))
            }),
            Expires = _clock.GetCurrentTime().Add(_jwtConfig.LifeTime).DateTime,
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