using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KutuphaneOtomasyonu.Models;

namespace KutuphaneOtomasyonu.Controllers
{
    public class YayinEviController : Controller
    {
        private KütüphaneEntities1 db = new KütüphaneEntities1();

        // GET: YayinEvi
        public ActionResult Index()
        {
            return View(db.YayinEvi.ToList());
        }

        // GET: YayinEvi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YayinEvi yayinEvi = db.YayinEvi.Find(id);
            if (yayinEvi == null)
            {
                return HttpNotFound();
            }
            return View(yayinEvi);
        }

        // GET: YayinEvi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: YayinEvi/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "yayineviID,yayineviAd,yayineviTel,yayineviAdres")] YayinEvi yayinEvi)
        {
            if (ModelState.IsValid)
            {
                db.YayinEvi.Add(yayinEvi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yayinEvi);
        }

        // GET: YayinEvi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YayinEvi yayinEvi = db.YayinEvi.Find(id);
            if (yayinEvi == null)
            {
                return HttpNotFound();
            }
            return View(yayinEvi);
        }

        // POST: YayinEvi/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "yayineviID,yayineviAd,yayineviTel,yayineviAdres")] YayinEvi yayinEvi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yayinEvi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yayinEvi);
        }

        // GET: YayinEvi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YayinEvi yayinEvi = db.YayinEvi.Find(id);
            if (yayinEvi == null)
            {
                return HttpNotFound();
            }
            return View(yayinEvi);
        }

        // POST: YayinEvi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YayinEvi yayinEvi = db.YayinEvi.Find(id);
            db.YayinEvi.Remove(yayinEvi);
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
    }
}
