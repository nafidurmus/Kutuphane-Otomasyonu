using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
using KutuphaneOtomasyonu.Models;
using System.Threading.Tasks;
using System.Net;

namespace KutuphaneOtomasyonu.Controllers
{
    public class HomeController : Controller
    {
        private KütüphaneEntities1 db = new KütüphaneEntities1();
        private static String kitapAdis = "";

        public ActionResult Index()
        {
         
            /*Models.Uye uye = new Models.Uye();
            uye.uyeAd = "kemald";
            uye.uyeSifre = "123";
            uye.uyeSoyad = "sari";
            uye.uyeTel = "1231245123";

            Models.Kitap kitap = new Models.Kitap();*/
            return View();
        }
        [HttpPost]
        public ActionResult Index(string kitapAdi)
        {
            kitapAdis = kitapAdi;
            var kitaplar = db.Kitap.Where(degisken => degisken.kitapAd.Contains(kitapAdi)).ToList();
            
            return View("AramaSonucu", kitaplar.ToList());
            //return Content(kitapAdi);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult CezaOde()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Iade(int? id)
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

        [HttpPost]
        public ActionResult Iade(int?[] idler)
        {  
            if(idler != null) {
                //System.Diagnostics.Debug.Write("Selam " + id + " " + kullaniciId);
                for (int i = 0; i< idler.Length; i++)
                {
                    OduncAlma o = db.OduncAlma.Find(idler[i]);
                    o.Kitap.kitapStok = true;
                    db.Entry(o).State = EntityState.Modified;
                    //db.SaveChanges();
                    db.OduncAlma.Remove(o);
                }
                db.SaveChanges();
                TempData["msg"] = "<script>alert('İade işlemi başarılı.');</script>";
                return RedirectToAction("Iade", "Home", User.Identity.Name.Split('|')[1]);
            }else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> OduncAl(int id, int kullaniciId)
        {
            DateTime bastarih = new DateTime(2018, 04, 20);
            DateTime bittarih = new DateTime(2018, 05, 1);
            int gecikmeSuresi = Int32.Parse((bittarih - bastarih).TotalDays.ToString());
            /*//Toplu silme Komutu
            foreach(Kitap k in db.Kitap)
            {
                k.kitapStok = true;
                db.Entry(k).State = EntityState.Modified;
            }
            foreach (OduncAlma z in db.OduncAlma) { 
                db.OduncAlma.Remove(z);
            }
            db.SaveChanges();*/

            //Ödünç alınan kitabın bilgilerini veritabanına girer.
            OduncAlma odunc = new OduncAlma();
            odunc.oduncID = id;
            odunc.uyeID = kullaniciId;
            odunc.kitapID = id;
            odunc.oduncAlmaTarihi = bastarih;
            odunc.oduncTeslimTarihi = bittarih;
            odunc.gecikmeSuresi = gecikmeSuresi;
            odunc.cezaBedeli = gecikmeSuresi *(1/4);

            System.Diagnostics.Debug.WriteLine("selam " + odunc.oduncID + " " + odunc.cezaBedeli + " " + odunc.gecikmeSuresi + " ");
            if (ModelState.IsValid)
            {
                db.OduncAlma.Add(odunc);
                db.SaveChanges();
            }

            //Kitap stok bilgisini ödünçte olarak değiştirir (false yapar).
            Kitap kitap = db.Kitap.Find(id);
            kitap.kitapStok = false;
            if (ModelState.IsValid)
            {
                db.Entry(kitap).State = EntityState.Modified;
                db.SaveChanges();
            }

            //Kitapların güncel durumunu sorgulayıp, arama sayfasına yeniden yükler.
            var kitaplar = db.Kitap.Where(degisken => degisken.kitapAd.Contains(kitapAdis)).ToList();
            return View("AramaSonucu", kitaplar.ToList());
        }
    }
}