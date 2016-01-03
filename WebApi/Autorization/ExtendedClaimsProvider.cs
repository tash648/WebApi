using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace QuickErrandsWebApi
{
    public static class IdentityUserExtension
    {
        public static IEnumerable<Claim> GetClaims(this IdentityUser user)
        {
            return user.Claims.Select(p => new Claim(p.ClaimType, p.ClaimValue));
        }
    }
}