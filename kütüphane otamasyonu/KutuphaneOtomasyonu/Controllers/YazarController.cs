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
    public class YazarController : Controller
    {
        private KütüphaneEntities1 db = new KütüphaneEntities1();

        // GET: Yazars
        public ActionResult Index()
        {
            return View(db.Yazar.ToList());
        }

        // GET: Yazars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yazar yazar = db.Yazar.Find(id);
            if (yazar == null)
            {
                return HttpNotFound();
            }
            return View(yazar);
        }

        // GET: Yazars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Yazars/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "yazarID,yazarAd,yazarSoyad")] Yazar yazar)
        {
            if (yazar.yazarSoyad == null || yazar.yazarSoyad == "")
                yazar.yazarSoyad = " ";
            if (ModelState.IsValid)
            {
                db.Yazar.Add(yazar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yazar);
        }

        // GET: Yazars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yazar yazar = db.Yazar.Find(id);
            if (yazar == null)
            {
                return HttpNotFound();
            }
            return View(yazar);
        }

        // POST: Yazars/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "yazarID,yazarAd,yazarSoyad")] Yazar yazar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yazar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yazar);
        }

        // GET: Yazars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yazar yazar = db.Yazar.Find(id);
            if (yazar == null)
            {
                return HttpNotFound();
            }
            return View(yazar);
        }

        // POST: Yazars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yazar yazar = db.Yazar.Find(id);
            db.Yazar.Remove(yazar);
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
