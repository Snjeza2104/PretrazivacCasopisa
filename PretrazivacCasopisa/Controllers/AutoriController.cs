using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PretrazivacCasopisa.Models;

namespace PretrazivacCasopisa.Controllers
{
    public class AutoriController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Autori
        public ActionResult Index( string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Ime_desc" : "";
            var autori = from s in db.Autoris
                         select s;
            switch (sortOrder)
            {
                case "Ime_desc":
                    autori = autori.OrderByDescending(s => s.ImeIPrezime);
                           break;
                default:  // Name ascending 
                    autori = autori.OrderBy(s => s.ImeIPrezime);
                    break;

            }
            return View(autori.ToList());
        }

        // GET: Autori/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autori autori = db.Autoris.Find(id);
            if (autori == null)
            {
                return HttpNotFound();
            }
            return View(autori);
        }

        // GET: Autori/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autori/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ImeIPrezime")] Autori autori)
        {
            if (ModelState.IsValid)
            {
                db.Autoris.Add(autori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(autori);
        }

        // GET: Autori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autori autori = db.Autoris.Find(id);
            if (autori == null)
            {
                return HttpNotFound();
            }
            return View(autori);
        }

        // POST: Autori/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ImeIPrezime")] Autori autori)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autori).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autori);
        }

        // GET: Autori/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autori autori = db.Autoris.Find(id);
            if (autori == null)
            {
                return HttpNotFound();
            }
            return View(autori);
        }

        // POST: Autori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Autori autori = db.Autoris.Find(id);
            db.Autoris.Remove(autori);
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
