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
using PagedList;

namespace MyProject.Controllers
{

    [Authorize]
    public class RecipeLikesController : Controller
    {
        private EBookContext db = new EBookContext();



        [Authorize]
        public RedirectToRouteResult Unlike([Bind(Include = "Id,RecipeID,ProfileID")] RecipeLike recipeLike, [Bind(Include = "RecipeID")] int recipeID)
        {
            int profileId = db.Profiles.Single(p => p.Login == User.Identity.Name).ID;
            db.RecipeLikes.Remove(db.RecipeLikes.Single(r => r.RecipeID == recipeID && r.ProfileID == profileId));
            db.SaveChanges();

            return RedirectToAction("Details", "Recipes", new { id = recipeID });
        }

        [Authorize]
        public RedirectToRouteResult Like([Bind(Include = "Id,RecipeID,ProfileID")] RecipeLike recipeLike, [Bind(Include = "RecipeID")] int recipeID)
        {
            recipeLike.ProfileID = db.Profiles.Single(p => p.Login== User.Identity.Name).ID;
            db.RecipeLikes.Add(recipeLike);
            db.SaveChanges();

            return RedirectToAction("Details", "Recipes", new { id = recipeID });
        }

        // GET: RecipeLikes
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, string categoryFilter, string typesFilter, int? page)

        {
            ViewBag.Categories = db.Categories;
            ViewBag.CategoryFilter = categoryFilter;

            ViewBag.Types = db.Types;
            ViewBag.TypeFilter = typesFilter;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CategorySortParm = sortOrder == "Categories" ? "categories_desc" : "Categories";
            ViewBag.RateSortParm = sortOrder == "Ratings" ? "ratings_desc" : "Ratings";

            var recipeLikes = db.RecipeLikes.Include(r => r.Profile).Include(r => r.Recipe);

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                recipeLikes = recipeLikes.Where(r => r.Recipe.Name.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(categoryFilter))
            {
                recipeLikes = recipeLikes.Where(s => s.Recipe.Categories.Name.Equals(categoryFilter));
            }
            if (!String.IsNullOrEmpty(typesFilter))
            {
                recipeLikes = recipeLikes.Where(s => s.Recipe.Types.Name.Equals(typesFilter));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    recipeLikes = recipeLikes.OrderByDescending(r => r.Recipe.Name);
                    break;
                case "Categories":
                    recipeLikes = recipeLikes.OrderBy(c => c.Recipe.Categories.Name);
                    break;
                case "categories_desc":
                    recipeLikes = recipeLikes.OrderByDescending(c => c.Recipe.Categories.Name);
                    break;
                case "Ratings":
                    recipeLikes = recipeLikes.OrderBy(re => re.Recipe.Ratings.Average(r => r.Rate));
                    break;
                case "ratings_desc":
                    recipeLikes = recipeLikes.OrderByDescending(re => re.Recipe.Ratings.Average(r => r.Rate));
                    break;
                default:
                    recipeLikes = recipeLikes.OrderBy(s => s.Recipe.Name);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(recipeLikes.ToPagedList(pageNumber, pageSize));

           
        }

        // GET: RecipeLikes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeLike recipeLike = db.RecipeLikes.Find(id);
            if (recipeLike == null)
            {
                return HttpNotFound();
            }
            return View(recipeLike);
        }

        // GET: RecipeLikes/Create
        public ActionResult Create()
        {
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login");
            ViewBag.RecipeID = new SelectList(db.Recipes, "ID", "Name");
            return View();
        }

        // POST: RecipeLikes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RecipeID,ProfileID")] RecipeLike recipeLike)
        {
            if (ModelState.IsValid)
            {
                db.RecipeLikes.Add(recipeLike);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", recipeLike.ProfileID);
            ViewBag.RecipeID = new SelectList(db.Recipes, "ID", "Name", recipeLike.RecipeID);
            return View(recipeLike);
        }

        // GET: RecipeLikes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeLike recipeLike = db.RecipeLikes.Find(id);
            if (recipeLike == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", recipeLike.ProfileID);
            ViewBag.RecipeID = new SelectList(db.Recipes, "ID", "Name", recipeLike.RecipeID);
            return View(recipeLike);
        }

        // POST: RecipeLikes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RecipeID,ProfileID")] RecipeLike recipeLike)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipeLike).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", recipeLike.ProfileID);
            ViewBag.RecipeID = new SelectList(db.Recipes, "ID", "Name", recipeLike.RecipeID);
            return View(recipeLike);
        }

        // GET: RecipeLikes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeLike recipeLike = db.RecipeLikes.Find(id);
            if (recipeLike == null)
            {
                return HttpNotFound();
            }
            return View(recipeLike);
        }

        // POST: RecipeLikes/Delete/5
        public ActionResult Delete2(int id)
        {
            RecipeLike recipeLike = db.RecipeLikes.Find(id);
            db.RecipeLikes.Remove(recipeLike);
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
