using KutuphaneOtomasyonu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;

namespace KutuphaneOtomasyonu.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (String.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                FormsAuthentication.SignOut();
                return View();
            }
            return Redirect("/Admin/Home");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnurl)
        {
            if (ModelState.IsValid)
            {
                //RepositoryPortal<AdminUser> rpstryadmn = new RepositoryPortal<AdminUser>();
                KütüphaneEntities1 db = new KütüphaneEntities1();
                var kullanici = db.Uye.Where(degisken => degisken.uyeAd == model.isim && degisken.uyeSifre == model.Password);
                //Aşağıdaki if komutu gönderilen mail ve şifre doğrultusunda kullanıcı kontrolu yapar. Eğer kullanıcı var ise login olur.
                if (kullanici.Count() > 0)
                {
                    FormsAuthentication.SetAuthCookie(model.isim, true);
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "Ad veya şifre hatalı!");
                }
            }
            return View(model);
        }
    }
}