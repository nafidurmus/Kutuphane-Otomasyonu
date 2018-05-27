using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KutuphaneOtomasyonu.Models;
using System.Web.Security;

namespace KutuphaneOtomasyonu.Controllers
{
    public class UyeController : Controller
    {
        private KütüphaneEntities1 db = new KütüphaneEntities1();

        // GET: Uye
        public ActionResult Index()
        {
            return View(db.Uye.ToList());
        }

        // GET: Uye/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = db.Uye.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // GET: Uye/Create
        /*public ActionResult Login()
        {
            return View();
        }*/

        // POST: Uye/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "uyeID,uyeSifre,uyeAd,uyeSoyad,uyeTel")] Uye uye)
        {
            if (ModelState.IsValid)
            {
                db.Uye.Add(uye);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(uye);
        }

        // GET: Uye/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = db.Uye.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // POST: Uye/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "uyeID,uyeSifre,uyeAd,uyeSoyad,uyeTel")] Uye uye)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uye).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uye);
        }

        // GET: Uye/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = db.Uye.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // POST: Uye/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uye uye = db.Uye.Find(id);
            db.Uye.Remove(uye);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }











        [AllowAnonymous]
        public ActionResult Login()
        {
            if (String.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                FormsAuthentication.SignOut();
                return View();
            }
            return Redirect("/Home/Index");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Giris(AuthModel model)
        {
            
            if (ModelState.IsValid)
            {
                //RepositoryPortal<AdminUser> rpstryadmn = new RepositoryPortal<AdminUser>();
                var kullanici = db.Uye.Where(degisken => degisken.uyeAd == model.login.isim && degisken.uyeSifre == model.login.Password);

                //İsme ve şifreye göre sorgu yapıp, bulduğu ilk queryi uye tipinden bir değişkene atar.
                //İd, isim gibi gereken değerleri cookie üzerinden alabilmek için bunu yaptık.
                Uye uye = db.Uye.FirstOrDefault(degisken => degisken.uyeAd == model.login.isim && degisken.uyeSifre == model.login.Password);
                
                //Aşağıdaki if komutu gönderilen mail ve şifre doğrultusunda kullanıcı kontrolu yapar. Eğer kullanıcı var ise login olur.
                if (kullanici.Count() > 0)
                {
                    FormsAuthentication.SetAuthCookie(model.login.isim+ "|" + uye.uyeID + "|" + uye.uyeSoyad, true);
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    //System.Diagnostics.Debug.WriteLine("Mrhba");
                    ModelState.AddModelError("Hata", "İsim veya şifre hatalı!");
                }
            }
            return View("Login");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Uye");
        }
    }
}
