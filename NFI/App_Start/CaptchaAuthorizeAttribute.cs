using System.Web;
using System.Web.Mvc;

namespace NFI.App_Start
{
    public class CaptchaAuthorizeAttribute : AuthorizeAttribute
    {
       protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var captchaVerified = false;
            if (httpContext.Session?["IsCaptchaVerfied"] != null)
            {
                captchaVerified = (bool)httpContext.Session["IsCaptchaVerfied"];
            }
            return captchaVerified;
        }
    }
}