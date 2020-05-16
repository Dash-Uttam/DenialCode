using DanialProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: OwinStartupAttribute(typeof(DanialProject.Startup))]
namespace DanialProject
{
    public partial class Startup
    {
        public Startup()
        {
        }

        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<ApplicationDbContext>(new Func<ApplicationDbContext>(ApplicationDbContext.Create));
            app.CreatePerOwinContext<ApplicationUserManager>(new Func<IdentityFactoryOptions<ApplicationUserManager>, IOwinContext, ApplicationUserManager>(ApplicationUserManager.Create));
            app.CreatePerOwinContext<ApplicationSignInManager>(new Func<IdentityFactoryOptions<ApplicationSignInManager>, IOwinContext, ApplicationSignInManager>(ApplicationSignInManager.Create));
            IAppBuilder app1 = app;
            CookieAuthenticationOptions options = new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Home/Index"),
                Provider = (ICookieAuthenticationProvider)new CookieAuthenticationProvider()
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(TimeSpan.FromMinutes(525600.0), (Func<ApplicationUserManager, ApplicationUser, Task<ClaimsIdentity>>)((manager, user) => user.GenerateUserIdentityAsync((UserManager<ApplicationUser>)manager))),
                    OnApplyRedirect = (Action<CookieApplyRedirectContext>)(ctx =>
                    {
                        if (Startup.IsAjaxRequest(ctx.Request))
                            return;
                        ctx.Response.Redirect(ctx.RedirectUri);
                    })
                }
            };
            app1.UseCookieAuthentication(options);
            app.UseExternalSignInCookie("ExternalCookie");
            app.UseTwoFactorSignInCookie("TwoFactorCookie", TimeSpan.FromMinutes(525600.0));
            app.UseTwoFactorRememberBrowserCookie("TwoFactorRememberBrowser");
        }

        private static bool IsAjaxRequest(IOwinRequest request)
        {
            IReadableStringCollection query = request.Query;
            if (query != null && query["X-Requested-With"] == "XMLHttpRequest")
                return true;
            IHeaderDictionary headers = request.Headers;
            return headers != null && headers["X-Requested-With"] == "XMLHttpRequest";
        }

    }
}
