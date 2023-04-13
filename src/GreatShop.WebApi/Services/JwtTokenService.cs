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
            Subject = CreateClaimsIdentity(account),
            Expires = _clock.GetCurrentTime().Add(_jwtConfig.LifeTime).DateTime,
            Audience = _jwtConfig.Audience,
            Issuer = _jwtConfig.Issuer,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_jwtConfig.SigningKeyBytes),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private ClaimsIdentity CreateClaimsIdentity(Account account)
    {
        var claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, account.Id.ToString())
        });
        foreach (var role in account.Roles)
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
        }

        return claimsIdentity;
    }
}