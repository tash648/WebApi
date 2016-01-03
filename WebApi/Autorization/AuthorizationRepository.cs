using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using QuickErrandsWebApi.Models;

namespace QuickErrandsWebApi
{
    public class AuthorizationRepository : IDisposable
    {
        private bool isDisposed;

        private AuthorizationContext context;
        private UserManager<IdentityUser> userManager;

        public AuthorizationRepository()
        {
            context = new AuthorizationContext();

            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };            

            foreach(var role in userModel.Roles)
            {
                var claim = new IdentityUserClaim() { ClaimType = ClaimTypes.Role, ClaimValue = role, UserId = user.Id };                

                user.Claims.Add(claim);
            }            

            return await userManager.CreateAsync(user, userModel.Password);            
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await userManager.FindAsync(userName, password);

            return user;
        }

        public async Task<IdentityUser> FindByName(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }

        public async Task UpdateUser(IdentityUser user)
        {
            await userManager.UpdateAsync(user);
        }

        public void Dispose()
        {
            if(!isDisposed)
            {
                context.Dispose();
                userManager.Dispose();
                isDisposed = true;
            }
        }
    }
}