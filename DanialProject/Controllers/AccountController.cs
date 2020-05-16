using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DanialProject.Models;
using System.Web.Security;
using DanialProject.Models.Database;
using DanialProject.Models.DataClasses;

namespace DanialProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;

        private ApplicationUserManager _userManager;

        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return base.HttpContext.GetOwinContext().Authentication;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return this._signInManager ?? base.HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                this._signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this._userManager ?? base.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                this._userManager = value;
            }
        }

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                base.ModelState.AddModelError("", error);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._userManager != null)
                {
                    this._userManager.Dispose();
                    this._userManager = null;
                }
                if (this._signInManager != null)
                {
                    this._signInManager.Dispose();
                    this._signInManager = null;
                }
            }
            base.Dispose(disposing);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            ((dynamic)base.ViewBag).Error = base.TempData["Error"];
            return base.View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginCheck(LoginViewModel model)
        {
            ActionResult action;
            if (!base.ModelState.IsValid)
            {
                base.TempData["Error"] = "Please Enter the Records Carefully";
                return base.RedirectToAction("Login", "Account");
            }
            try
            {
                Admins user = SecurityClass.GetLoginStatus(model);
                if (user == null)
                {
                    base.TempData["Error"] = "Username or password is Incorrect!";
                    action = base.RedirectToAction("Login", "Account");
                }
                else
                {
                    string role = "";
                    try
                    {
                        if (user.SecurityLevelID.HasValue)
                        {
                            role = SecurityClass.GetAllAccessPass(user.SecurityLevelID.Value);
                        }
                        else if (user.SecurityLevelID.HasValue || !(user.Occupation == "MasterAdmin"))
                        {
                            base.TempData["Error"] = "Username or password is Incorrect!";
                            action = base.RedirectToAction("Login", "Account");
                            return action;
                        }
                        else
                        {
                            role = "MasterAdmin,Roles,Admins,Agents,Features,Approved,Pending,Rejected";
                        }
                    }
                    catch (Exception)
                    {
                        role = "";
                    }
                    DateTime cookieIssuedDate = DateTime.Now;
                    string str = user.ID.ToString();
                    DateTime now = DateTime.Now;
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, str, cookieIssuedDate, now.AddMinutes(525600), false, role);
                    HttpCookie httpCookie = new HttpCookie("AdminEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd", FormsAuthentication.Encrypt(ticket))
                    {
                        Domain = FormsAuthentication.CookieDomain,
                        Path = FormsAuthentication.FormsCookiePath,
                        HttpOnly = true,
                        Secure = FormsAuthentication.RequireSSL
                    };
                    //HttpContext.Current.Response.Cookies.Add(httpCookie);
                    this.ControllerContext.HttpContext.Response.Cookies.Add(httpCookie);
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    action = base.RedirectToAction("WelcomePage", "Admin");
                }
            }
            catch (Exception)
            {
                base.TempData["Error"] = "Username or password is Incorrect!";
                action = base.RedirectToAction("Login", "Account");
            }
            return action;
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            HttpCookie httpCookie = new HttpCookie("AdminEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd")
            {
                Expires = DateTime.Now.AddDays(-525600)
            };
            base.Response.Cookies.Add(httpCookie);
            this.AuthenticationManager.SignOut(new string[] { "ApplicationCookie" });
            return base.RedirectToAction("WelcomePage", "Admin");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (base.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            return base.RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult SignOut()
        {
            HttpCookie httpCookie = new HttpCookie("AgentEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd")
            {
                Expires = DateTime.Now.AddDays(-525600)
            };
            base.Response.Cookies.Add(httpCookie);
            this.AuthenticationManager.SignOut(new string[] { "ApplicationCookie" });
            return base.RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public string LoginProvider
            {
                get;
                set;
            }

            public string RedirectUri
            {
                get;
                set;
            }

            public string UserId
            {
                get;
                set;
            }

            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                this.LoginProvider = provider;
                this.RedirectUri = redirectUri;
                this.UserId = userId;
            }

            public override void ExecuteResult(ControllerContext context)
            {
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    RedirectUri = this.RedirectUri
                };
                if (this.UserId != null)
                {
                    properties.Dictionary["XsrfId"] = this.UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, new string[] { this.LoginProvider });
            }
        }
    }
}