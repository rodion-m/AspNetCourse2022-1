using System.Security.Claims;

namespace Lesson21.UoW;

public static class PrincipalExtensions
{
    public static Guid GetAccountId(this ClaimsPrincipal principal)
    {
        var strId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        var guid = Guid.Parse(strId);
        return guid;
    }
}