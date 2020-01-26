using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MJC_Blogs.Models;

namespace MJC_Blogs.Controllers
{
    [RequireHttps]
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public ActionResult Index()
        {
            var comments = db.Comment.Include(c => c.Author).Include(c => c.Post);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id, string returnSlug)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comment.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Title");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PostId,AuthorId,Body,Created")] Comments comments, int postId)
        {
            if (ModelState.IsValid)
            {
                comments.PostId = postId;
                comments.AuthorId = User.Identity.GetUserId();
                comments.Created = DateTimeOffset.Now;
                db.Comment.Add(comments);
                db.SaveChanges();

                var slug = db.Posts.FirstOrDefault(p => p.Id == postId).Slug;
                return RedirectToAction("Details", "Blogs", new { Slug = slug});
            }

            return View(comments);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comment.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }

            return View(comments);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PostId,AuthorId,Body,Created,Updated,UpdateReason")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                comments.Updated = DateTimeOffset.Now;
                db.Entry(comments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Blogs", new { slug = comments.Post.Slug});
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName", comments.AuthorId);
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Title", comments.PostId);
            return View(comments);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comment.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            
            return View(comments);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comments comments = db.Comment.Find(id);
            var postSlug = comments.Post.Slug;
            db.Comment.Remove(comments);
            db.SaveChanges();
            return RedirectToAction("Details", "Blogs", new { slug = postSlug } );
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
