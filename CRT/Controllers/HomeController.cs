using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRT;

namespace CRT.Controllers
{
    public class HomeController : Controller
    {
        private DataEntities db = new DataEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View(db.iProducts.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iProducts iProducts = db.iProducts.Find(id);
            if (iProducts == null)
            {
                return HttpNotFound();
            }
            return View(iProducts);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return Redirect("/");
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iProducts iProducts = db.iProducts.Find(id);
            if (iProducts == null)
            {
                return HttpNotFound();
            }
            return View(iProducts);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,Img")] iProducts iProducts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iProducts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iProducts);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iProducts iProducts = db.iProducts.Find(id);
            if (iProducts == null)
            {
                return HttpNotFound();
            }
            return View(iProducts);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            iProducts iProducts = db.iProducts.Find(id);
            db.iProducts.Remove(iProducts);
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
