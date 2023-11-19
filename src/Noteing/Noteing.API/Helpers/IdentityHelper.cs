using Duende.IdentityServer.Extensions;
using System.Security.Claims;

namespace Noteing.API.Helpers
{
    internal static class IdentityHelper
    {

        internal static Guid GetCurrentUserId(ClaimsPrincipal principal)
        {
            return Guid.Parse(principal.Identity.GetSubjectId());
        }
    }
}
