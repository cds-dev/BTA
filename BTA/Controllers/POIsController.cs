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
using System.Configuration;

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
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "poiId,city,name,address,website,poiImg,rating,lon,lat,phone,email,category")] POI pOI)
        {
            var url = Uri.EscapeDataString(pOI.website);
            var ogKey = Environment.ExpandEnvironmentVariables(
                    ConfigurationManager.AppSettings["OpenGraphAPI"]);
            var requestUrl = "https://opengraph.io/api/1.1/site/" + url + "?app_id=" + ogKey;
            dynamic ogResults = new Uri(requestUrl).GetDynamicJsonObject();

            pOI.name = Convert.ToString(ogResults.hybridGraph.title);

            pOI.rating = Convert.ToDouble(pOI.name.IndexOf(' '));
            pOI.pOIDescription = Convert.ToString(ogResults.hybridGraph.description);
            pOI.poiImg = Convert.ToString(ogResults.hybridGraph.image);

            string gcUrl = "https://maps.googleapis.com/maps/api/geocode/json?sensor=true&address=";
            string gcKey = Environment.ExpandEnvironmentVariables(
                    ConfigurationManager.AppSettings["GoogleAPI"]);

            string key = "&key=" + gcKey;

            dynamic googleResults = new Uri(gcUrl + pOI.name + key).GetDynamicJsonObject();
            pOI.poiId = Convert.ToString(googleResults.results[0].place_id);
            //var city = Convert.ToString(googleResults.results[0].place_id);
            pOI.lon = Convert.ToDouble(googleResults.results[0].geometry.location.lng);
            pOI.lat = Convert.ToDouble(googleResults.results[0].geometry.location.lat);
            pOI.address = Convert.ToString(googleResults.results[0].formatted_address);


            //google geocoding api
            //string gcUrl = "https://maps.googleapis.com/maps/api/geocode/json?sensor=true&address=";
            //string key = "&key=" + Environment.ExpandEnvironmentVariables(
            //        ConfigurationManager.AppSettings["GoogleAPI"]);

            //string key = "&key=" + apiKey;

            //dynamic googleResults = new Uri(gcUrl + poiName + key).GetDynamicJsonObject();

            //pOI.poiId = Convert.ToString(googleResults.results[0].place_id);
            //pOI.name = Convert.ToString(googleResults.results[0].address_components[0].long_name);

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
