using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;
using QuickErrandsWebApi.DependencyResolver;
using System.ComponentModel.Composition.Hosting;

[assembly: OwinStartup(typeof(QuickErrandsWebApi.Startup))]
namespace QuickErrandsWebApi
{
    public class Startup
    {
        #region Private

        private CompositionContainer GetCompositionContainer()
        {
            return new CompositionContainer(new AssemblyCatalog(GetType().Assembly));
        }

        private void ConfigureMef(HttpConfiguration configuration)
        {
            var resolver = new MefDependencyResolver(GetCompositionContainer());

            configuration.DependencyResolver = resolver;
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(15),
                Provider = new AuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        #endregion

        #region Public

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            ConfigureOAuth(app);
            ConfigureMef(config);

            WebApiConfig.Register(config);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(config);
        } 

        #endregion
    }
}