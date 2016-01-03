using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using QuickErrandsWebApi.Attributes;
using QuickErrandsWebApi.BindingModels;
using QuickErrandsWebApi.Models;
using QuickErrandsWebApi.Models.Roles;

namespace QuickErrandsWebApi.Controllers
{
    [RoutePrefix("api/v1/Account")]
    public class AccountController : ApiController
    {
        #region Private

        private AuthorizationRepository repository = null;

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        #endregion

        #region Protected

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Public

        public AccountController()
        {
            repository = new AuthorizationRepository();
        }

        [AllowAnonymous]
        [Route("register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await repository.FindUser(userModel.UserName, userModel.Password);

            if (user != null)
            {
                return BadRequest();
            }

            IdentityResult result = await repository.RegisterUser(userModel);

            var errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        [RoleAuthorize(typeof(Admin))]
        [HttpPut]
        [Route("roles")]
        public async Task<IHttpActionResult> AddRolesToUser(UpdateUserModel userModel)
        {
            var user = await repository.FindByName(userModel.UserName);

            if (user == null)
            {
                return NotFound();
            }

            foreach (var role in userModel.Roles)
            {
                if (user.Claims.FirstOrDefault(p => p.ClaimType == ClaimTypes.Role && p.ClaimValue == role) == null)
                {
                    user.Claims.Add(new IdentityUserClaim() { ClaimType = ClaimTypes.Role, ClaimValue = role, UserId = user.Id });
                }
            }

            await repository.UpdateUser(user);

            return Ok();
        } 

        #endregion
    }
}
