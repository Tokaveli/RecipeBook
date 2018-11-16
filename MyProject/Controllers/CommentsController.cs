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
    public class CommentsController : Controller
    {
        private EBookContext db = new EBookContext();

        // GET: Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Profile).Include(c => c.Recipe);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login");
            ViewBag.RecipeID = new SelectList(db.Recipes, "ID", "Name");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public ActionResult Create( Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Profile = db.Profiles.Single(p => p.Login == User.Identity.Name);
                comment.CommentDate= DateTime.Now;
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details","Recipes",new { id = comment.RecipeID });
            }

            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", comment.ProfileID);
            ViewBag.RecipeID = new SelectList(db.Recipes, "ID", "Name", comment.RecipeID);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", comment.ProfileID);
            ViewBag.RecipeID = new SelectList(db.Recipes, "ID", "Name", comment.RecipeID);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Content,CommentDate,RecipeID,ProfileID")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", comment.ProfileID);
            ViewBag.RecipeID = new SelectList(db.Recipes, "ID", "Name", comment.RecipeID);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete2(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
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
