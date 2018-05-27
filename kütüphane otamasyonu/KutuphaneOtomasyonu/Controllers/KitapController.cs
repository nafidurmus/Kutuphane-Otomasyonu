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
    public class KitapController : Controller
    {
        private KütüphaneEntities1 db = new KütüphaneEntities1();

        // GET: Kitap
        public ActionResult Index()
        {
            var kitap = db.Kitap.Include(k => k.Tur).Include(k => k.YayinEvi).Include(k => k.Yazar);
            return View(kitap.ToList());
        }

        // GET: Kitap/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitap kitap = db.Kitap.Find(id);
            if (kitap == null)
            {
                return HttpNotFound();
            }
            return View(kitap);
        }

        // GET: Kitap/Create
        public ActionResult Create()
        {
            ViewBag.turID = new SelectList(db.Tur, "turID", "turAd");
            ViewBag.yayinEviID = new SelectList(db.YayinEvi, "yayineviID", "yayineviAd");
            ViewBag.yazarID = new SelectList(db.Yazar, "yazarID", "yazarAd");
            return View();
        }

        // POST: Kitap/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "kitapID,kitapAd,yazarID,yayinEviID,turID,kitapStok")] Kitap kitap)
        {
            //System.Diagnostics.Debug.WriteLine(kitap.turID + "  " + kitap.yazarID);
            if (ModelState.IsValid)
            {
                db.Kitap.Add(kitap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.turID = new SelectList(db.Tur, "turID", "turAd", kitap.turID);
            ViewBag.yayinEviID = new SelectList(db.YayinEvi, "yayineviID", "yayineviAd", kitap.yayinEviID);
            ViewBag.yazarID = new SelectList(db.Yazar, "yazarID", "yazarAd", kitap.yazarID);
            return View(kitap);
        }

        // GET: Kitap/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitap kitap = db.Kitap.Find(id);
            if (kitap == null)
            {
                return HttpNotFound();
            }
            ViewBag.turID = new SelectList(db.Tur, "turID", "turAd", kitap.turID);
            ViewBag.yayinEviID = new SelectList(db.YayinEvi, "yayineviID", "yayineviAd", kitap.yayinEviID);
            ViewBag.yazarID = new SelectList(db.Yazar, "yazarID", "yazarAd", kitap.yazarID);
            return View(kitap);
        }

        // POST: Kitap/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "kitapID,kitapAd,yazarID,yayinEviID,turID,kitapStok")] Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kitap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.turID = new SelectList(db.Tur, "turID", "turAd", kitap.turID);
            ViewBag.yayinEviID = new SelectList(db.YayinEvi, "yayineviID", "yayineviAd", kitap.yayinEviID);
            ViewBag.yazarID = new SelectList(db.Yazar, "yazarID", "yazarAd", kitap.yazarID);
            return View(kitap);
        }

        // GET: Kitap/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitap kitap = db.Kitap.Find(id);
            if (kitap == null)
            {
                return HttpNotFound();
            }
            return View(kitap);
        }

        // POST: Kitap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kitap kitap = db.Kitap.Find(id);
            db.Kitap.Remove(kitap);
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
