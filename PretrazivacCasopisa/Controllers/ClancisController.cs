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
    public class ClancisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clancis
        public ActionResult Index(string sortOrder, string currentFilter, string searchString)
        {
            var clancis = db.Clancis.Include(c => c.Autor).Include(c => c.Casopis2);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.snaslov = sortOrder == "snaslov_asc" ? "snaslov_desc" : "snaslov_asc";
            ViewBag.snaziv = sortOrder == "snaziv_asc" ? "snaziv_desc" : "snaziv_asc";
            ViewBag.sbroj = sortOrder == "sbroj_asc" ? "sbroj_desc" : "sbroj_asc";
            ViewBag.simeiprezime = sortOrder == "simeiprezime_asc" ? "simeiprezime_desc" : "simeiprezime_asc";

            if (searchString != null) { }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var clanci = from s in clancis select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                clanci=clanci.Where(s=> s.Naslov.Contains(searchString) || s.Casopis2.Naziv.Contains(searchString) || s.broj.ToString().Contains(searchString)
                    || s.Autor.ImeIPrezime.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "snaslov_desc":
                    clanci = clanci.OrderByDescending(s => s.Naslov);
                    break;
                case "snaslov_asc":
                    clanci = clanci.OrderBy(s => s.Naslov);
                    break;
                case "snaziv_desc":
                    clanci = clanci.OrderByDescending(s => s.Casopis2.Naziv);
                    break;
                case "snaziv_asc":
                    clanci = clanci.OrderBy(s => s.Casopis2.Naziv);
                    break;
                case "sbroj_desc":
                    clanci = clanci.OrderByDescending(s => s.broj);
                    break;
                case "sbroj_asc":
                    clanci = clanci.OrderBy(s => s.broj);
                    break;
                case "simeiprezime_desc":
                    clanci = clanci.OrderByDescending(s => s.Autor.ImeIPrezime);
                    break;
                case "simeiprezime_asc":
                    clanci = clanci.OrderBy(s => s.Autor.ImeIPrezime);
                    break;
                default:
                    break;
            }

            return View(clanci.ToList());
        }

        // GET: Clancis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clanci clanci = db.Clancis.Find(id);
            if (clanci == null)
            {
                return HttpNotFound();
            }
            return View(clanci);
        }

        // GET: Clancis/Create
        public ActionResult Create()
        {
            ViewBag.AutorID = new SelectList(db.Autoris, "Id", "ImeIPrezime");
            ViewBag.CasopisID = new SelectList(db.Casopis, "Id", "Naziv");
            return View();
        }

        // POST: Clancis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naslov,CasopisID,broj,AutorID")] Clanci clanci)
        {
            if (ModelState.IsValid)
            {
                db.Clancis.Add(clanci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AutorID = new SelectList(db.Autoris, "Id", "ImeIPrezime", clanci.AutorID);
            ViewBag.CasopisID = new SelectList(db.Casopis, "Id", "Naziv", clanci.CasopisID);
            return View(clanci);
        }

        // GET: Clancis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clanci clanci = db.Clancis.Find(id);
            if (clanci == null)
            {
                return HttpNotFound();
            }
            ViewBag.AutorID = new SelectList(db.Autoris, "Id", "ImeIPrezime", clanci.AutorID);
            ViewBag.CasopisID = new SelectList(db.Casopis, "Id", "Naziv", clanci.CasopisID);
            return View(clanci);
        }

        // POST: Clancis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naslov,CasopisID,broj,AutorID")] Clanci clanci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clanci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AutorID = new SelectList(db.Autoris, "Id", "ImeIPrezime", clanci.AutorID);
            ViewBag.CasopisID = new SelectList(db.Casopis, "Id", "Naziv", clanci.CasopisID);
            return View(clanci);
        }

        // GET: Clancis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clanci clanci = db.Clancis.Find(id);
            if (clanci == null)
            {
                return HttpNotFound();
            }
            return View(clanci);
        }

        // POST: Clancis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clanci clanci = db.Clancis.Find(id);
            db.Clancis.Remove(clanci);
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
