using Lesson14.Models;

namespace Lesson19.Auth.Services;

public interface ITokenService
{
    string GenerateToken(Account account);
}