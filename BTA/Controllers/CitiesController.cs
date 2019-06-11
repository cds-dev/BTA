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
using System.IO;
using static BTA.ApplicationSignInManager;

namespace BTA.Controllers
{
    public class CitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        // GET: Cities
        public async Task<ActionResult> Index()
        {
            return View(await db.Cities.ToListAsync());
        }

        // GET: Cities/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // GET: Cities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cityId,city1,country,population,lon,lat,image")] City city)
        {   
            var cityName = city.city1;

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3
                    | System.Net.SecurityProtocolType.Tls
                    | System.Net.SecurityProtocolType.Tls11
                    | System.Net.SecurityProtocolType.Tls12;

            //google geocoding api
            string gcUrl = "https://maps.googleapis.com/maps/api/geocode/json?sensor=true&address=";
            string apiKey = Environment.ExpandEnvironmentVariables(
                    ConfigurationManager.AppSettings["GoogleAPI"]);

            string key = "&key=" + apiKey;

            dynamic googleResults = new Uri(gcUrl + cityName + key).GetDynamicJsonObject();

            //opendatasoft api - population 
            string odUrl = "https://public.opendatasoft.com/api/records/1.0/search/?dataset=worldcitiespop&sort=population&facet=city&refine.city=" + cityName;
            dynamic populationResults = new Uri(odUrl).GetDynamicJsonObject();

            //google place get photo ref api
            //string photoRefUrl = "https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input=" + cityName + "&inputtype=textquery&fields=photos" + key;
            //dynamic photoRefResult = new Uri(photoRefUrl).GetDynamicJsonObject();
            //string photRef = Convert.ToString(photoRefResult.candidates[0].photos[0].photo_reference);

            ////google place photo api
            //string photoUrl = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=" + photRef + key;
            //dynamic photoResult = new Uri(photoUrl).GetDynamicJsonObject();

            //city.imageFile = photoResult;

            //if (photoResult != null)
            //{
            //    string extension = Path.GetExtension(photoResult.FileName);
            //    string photoName = cityName;
            //    string fileName = photoName + extension;
            //    city.imgUrl = "~/Assets/Images/Cities" + fileName;
            //    fileName = Path.Combine(Server.MapPath("~/Assets/Images/Cities/"), fileName);
            //    city.imageFile = photoResult;
            //    city.imageFile.SaveAs(fileName);
            //}
            //else
            //{
            //    city.imageFile = null;
            //}


            city.city1 = Convert.ToString(googleResults.results[0].address_components[0].long_name);
            city.cityId =  Convert.ToString(googleResults.results[0].place_id);
            city.country = Convert.ToString(googleResults.results[0].address_components[googleResults.results[0].address_components.Length-1].long_name);
            city.lat = Convert.ToDouble(googleResults.results[0].geometry.location.lat);
            city.lon = Convert.ToDouble(googleResults.results[0].geometry.location.lng);
            city.population = Convert.ToInt32(populationResults.records[0].fields.population);

            if (ModelState.IsValid)
            {
                db.Cities.Add(city);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cityId,city1,country,lon,lat,image,population")] City city)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(city);
        }

        // GET: Cities/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            City city = await db.Cities.FindAsync(id);
            db.Cities.Remove(city);
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
