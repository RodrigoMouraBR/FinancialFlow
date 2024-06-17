using System.Security.Claims;

namespace Carrefas.Core.Interfaces
{
    public interface IAspNetIdentityUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        bool IsAuthenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
