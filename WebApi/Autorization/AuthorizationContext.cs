using Microsoft.AspNet.Identity.EntityFramework;

namespace QuickErrandsWebApi
{
    public class AuthorizationContext : IdentityDbContext<IdentityUser>
    {
        public AuthorizationContext()
            : base("AuthorizationContext")
        {

        }
    }
}