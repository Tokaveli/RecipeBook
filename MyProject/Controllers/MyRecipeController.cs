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
    public class MyRecipeController : Controller


        {
            private EBookContext db = new EBookContext();

            // GET: Recipes
           public ActionResult Index(string sortOrder, string currentFilter, string searchString, string categoryFilter, string typesFilter, int? page,string name, Profile profile)
        {

                ViewBag.Categories = db.Categories;
                ViewBag.CategoryFilter = categoryFilter;

            ViewBag.Types = db.Types;
            ViewBag.TypeFilter = typesFilter;

            ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.CategorySortParm = sortOrder == "Categories" ? "categories_desc" : "Categories";
                ViewBag.RateSortParm = sortOrder == "Ratings" ? "ratings_desc" : "Ratings";



                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;
                var recipes = db.Recipes.Include(r => r.Categories).Include(r => r.Profile).Include(r => r.Types);

                if (!String.IsNullOrEmpty(searchString))
                {
                    recipes = recipes.Where(s => s.Name.Contains(searchString));
                }

                if (!String.IsNullOrEmpty(categoryFilter))
                {
                    recipes = recipes.Where(s => s.Categories.Name.Equals(categoryFilter));
                }

            if (!String.IsNullOrEmpty(typesFilter))
            {
                recipes = recipes.Where(s => s.Types.Name.Equals(typesFilter));
            }

            switch (sortOrder)
                {
                    case "name_desc":
                        recipes = recipes.OrderByDescending(r => r.Name);
                        break;
                    case "Categories":
                        recipes = recipes.OrderBy(c => c.Categories.Name);
                        break;
                    case "categories_desc":
                        recipes = recipes.OrderByDescending(c => c.Categories.Name);
                        break;
                    case "Ratings":
                        recipes = recipes.OrderBy(re => re.Ratings.Average(r => r.Rate));
                        break;
                    case "ratings_desc":
                        recipes = recipes.OrderByDescending(re => re.Ratings.Average(r => r.Rate));
                        break;
                    default:
                        recipes = recipes.OrderBy(s => s.Name);
                        break;
                }
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                return View(recipes.ToPagedList(pageNumber, pageSize));

            }

            // GET: Recipes/Details/5
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Recipe recipe = db.Recipes.Find(id);
                if (recipe == null)
                {
                    return HttpNotFound();
                }
                return View(recipe);
            }

            // GET: Recipes/Create
            public ActionResult Create()
            {
                ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
                ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login");
                ViewBag.TypeID = new SelectList(db.Types, "ID", "Name");
                return View();
            }

            // POST: Recipes/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "ID,Name,Description,Components,CategoryID,ProfileID,TypeID")] Recipe recipe)
            {
                HttpPostedFileBase file = Request.Files["fileWithPicture"];
                if (file != null && file.ContentLength > 0)
                {
                    recipe.Picture = System.Guid.NewGuid().ToString();
                    recipe.Picture = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Pictures/") + recipe.Picture);
                }

                if (ModelState.IsValid)
                {
                    db.Recipes.Add(recipe);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", recipe.CategoryID);
                ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", recipe.ProfileID);
                ViewBag.TypeID = new SelectList(db.Types, "ID", "Name", recipe.TypeID);
                return View(recipe);
            }

            // GET: Recipes/Edit/5
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Recipe recipe = db.Recipes.Find(id);
                if (recipe == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", recipe.CategoryID);
                ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", recipe.ProfileID);
                ViewBag.TypeID = new SelectList(db.Types, "ID", "Name", recipe.TypeID);
                return View(recipe);
            }

            // POST: Recipes/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "ID,Name,Description,Components,CategoryID,ProfileID,TypeID")] Recipe recipe)
            {
                HttpPostedFileBase file = Request.Files["fileWithPicture"];
                if (file != null && file.ContentLength > 0)
                {
                    recipe.Picture = System.Guid.NewGuid().ToString();
                    recipe.Picture = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Pictures/") + recipe.Picture);
                }

                if (ModelState.IsValid)
                {
                    db.Entry(recipe).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", recipe.CategoryID);
                ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", recipe.ProfileID);
                ViewBag.TypeID = new SelectList(db.Types, "ID", "Name", recipe.TypeID);
                return View(recipe);
            }

            // GET: Recipes/Delete/5
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Recipe recipe = db.Recipes.Find(id);
                if (recipe == null)
                {
                    return HttpNotFound();
                }
                return View(recipe);
            }

            // POST: Recipes/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                Recipe recipe = db.Recipes.Find(id);
                db.Recipes.Remove(recipe);
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
