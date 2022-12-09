using GreatShop.Domain.Entities;

namespace GreatShop.Domain.Services;

public interface ITokenService
{
    string GenerateToken(Account account);
}