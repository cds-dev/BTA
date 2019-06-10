using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTA.Models;

namespace BTA.Controllers
{
    public class POIsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: POIs
        public async Task<ActionResult> Index()
        {
            var pOIs = db.POIs.Include(p => p.Category1).Include(p => p.City1);
            return View(await pOIs.ToListAsync());
        }

        // GET: POIs/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POI pOI = await db.POIs.FindAsync(id);
            if (pOI == null)
            {
                return HttpNotFound();
            }
            return View(pOI);
        }

        // GET: POIs/Create
        public ActionResult Create()
        {
            ViewBag.category = new SelectList(db.Categories, "categoryId", "category1");
            ViewBag.city = new SelectList(db.Cities, "cityId", "city1");
            return View();
        }

        // POST: POIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "poiId,city,name,address,website,poiImg,rating,lon,lat,phone,email,category")] POI pOI)
        {
            if (ModelState.IsValid)
            {
                db.POIs.Add(pOI);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.category = new SelectList(db.Categories, "categoryId", "category1", pOI.category);
            ViewBag.city = new SelectList(db.Cities, "cityId", "city1", pOI.city);
            return View(pOI);
        }

        // GET: POIs/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POI pOI = await db.POIs.FindAsync(id);
            if (pOI == null)
            {
                return HttpNotFound();
            }
            ViewBag.category = new SelectList(db.Categories, "categoryId", "category1", pOI.category);
            ViewBag.city = new SelectList(db.Cities, "cityId", "city1", pOI.city);
            return View(pOI);
        }

        // POST: POIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "poiId,city,name,address,website,poiImg,rating,lon,lat,phone,email,category")] POI pOI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pOI).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.category = new SelectList(db.Categories, "categoryId", "category1", pOI.category);
            ViewBag.city = new SelectList(db.Cities, "cityId", "city1", pOI.city);
            return View(pOI);
        }

        // GET: POIs/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POI pOI = await db.POIs.FindAsync(id);
            if (pOI == null)
            {
                return HttpNotFound();
            }
            return View(pOI);
        }

        // POST: POIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            POI pOI = await db.POIs.FindAsync(id);
            db.POIs.Remove(pOI);
            await db.SaveChangesAsync();
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
