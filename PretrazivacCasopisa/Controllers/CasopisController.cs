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
    public class CasopisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Casopis
        public ActionResult Index(string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Ime_desc" : "";
            var casopisi = from s in db.Casopis
                         select s;
            switch (sortOrder)
            {
                case "Ime_desc":
                    casopisi = casopisi.OrderByDescending(s => s.Naziv);
                    break;
                default:
                    casopisi = casopisi.OrderBy(s => s.Naziv);
                    break;
            }
            return View(casopisi.ToList());
        }

        // GET: Casopis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Casopis casopis = db.Casopis.Find(id);
            if (casopis == null)
            {
                return HttpNotFound();
            }
            return View(casopis);
        }

        // GET: Casopis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Casopis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv")] Casopis casopis)
        {
            if (ModelState.IsValid)
            {
                db.Casopis.Add(casopis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(casopis);
        }

        // GET: Casopis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Casopis casopis = db.Casopis.Find(id);
            if (casopis == null)
            {
                return HttpNotFound();
            }
            return View(casopis);
        }

        // POST: Casopis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv")] Casopis casopis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(casopis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(casopis);
        }

        // GET: Casopis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Casopis casopis = db.Casopis.Find(id);
            if (casopis == null)
            {
                return HttpNotFound();
            }
            return View(casopis);
        }

        // POST: Casopis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Casopis casopis = db.Casopis.Find(id);
            db.Casopis.Remove(casopis);
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
