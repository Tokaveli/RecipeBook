using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyProject.DAL;
using MyProject.Models;

namespace MyProject.Controllers
{
    [Authorize]
    public class RatingsController : Controller
    {
        private EBookContext db = new EBookContext();

        // GET: Ratings
        public ActionResult Index()
        {
            var ratings = db.Ratings.Include(r => r.Profile).Include(r => r.Recipe);
            return View(ratings.ToList());
        }

        // GET: Ratings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // GET: Ratings/Create
        public ActionResult Create()
        {
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login");
            ViewBag.RecipeID = new SelectList(db.Recipes, "ID", "Name");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
     
        [HttpPost]
        [Authorize]
        public ActionResult Create(Rating rating)
        {
            if (ModelState.IsValid)
            {
                Profile profile = db.Profiles.Single(p => p.Login == User.Identity.Name);
                rating.Profile = profile;

                var ratings = db.Ratings.Where(r => r.RecipeID == rating.RecipeID && r.ProfileID == profile.ID);

                if (ratings.Count() > 0)
                {
                    ratings.First().Rate = rating.Rate;
                }
                else
                {
                    db.Ratings.Add(rating);
                }

                
                db.SaveChanges();
                return RedirectToAction("Details", "Recipes", new { id = rating.RecipeID });
            }

            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", rating.ProfileID);
            ViewBag.RecipeID = new SelectList(db.Recipes, "ID", "Name", rating.RecipeID);
            return View(rating);
        }

        // GET: Ratings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", rating.ProfileID);
            ViewBag.RecipeID = new SelectList(db.Recipes, "ID", "Name", rating.RecipeID);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Rate,RecipeID,ProfileID")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", rating.ProfileID);
            ViewBag.RecipeID = new SelectList(db.Recipes, "ID", "Name", rating.RecipeID);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rating rating = db.Ratings.Find(id);
            db.Ratings.Remove(rating);
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
