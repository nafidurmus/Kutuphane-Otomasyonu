using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KutuphaneOtomasyonu.Controllers
{
    class _SessionControlAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                    filterContext.HttpContext.Response.Redirect("/Uye/Login");

                //[_SessionControl]
                //Yukarıdaki komut yazıldığı yerde login olmadan sayfaya girişe izin vermemesini sağlıyor.
            }
        }
    }
}