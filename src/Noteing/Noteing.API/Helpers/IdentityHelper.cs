using Duende.IdentityServer.Extensions;
using System.Security.Claims;

namespace Noteing.API.Helpers
{
    internal static class IdentityHelper
    {

        internal static Guid GetCurrentUserId(ClaimsPrincipal principal)
        {
            try
            {
                return Guid.Parse(principal.Identity.GetSubjectId());
            }
            catch
            {
                return Guid.Empty;
            }
        }
    }
}
