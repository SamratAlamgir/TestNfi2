using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NFI.App_Start
{
    public class CaptchaAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            var captchaVerified = false;
            if (filterContext.HttpContext.Session?["IsCaptchaVerfied"] != null)
            {
                captchaVerified = (bool)filterContext.HttpContext.Session["IsCaptchaVerfied"];
            }
            if (!captchaVerified)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                            { "controller", "Captcha" },
                            { "action", "Index" },
                            { "ReturnUrl", filterContext.HttpContext.Request.RawUrl }
                    });
            }

        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return true;
        }
    }
}