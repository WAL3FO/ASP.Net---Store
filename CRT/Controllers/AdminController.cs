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
    public class AdminController : Controller
    {
        private DataEntities db = new DataEntities();

        // GET: Admin
        public ActionResult Index()
        {
            if(Request.IsAuthenticated && this.User.IsInRole("Admin")) {
                return View(db.iOrders.ToList());
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iOrders iOrders = db.iOrders.Find(id);
            if (iOrders == null)
            {
                return HttpNotFound();
            }
            return View(iOrders);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated && this.User.IsInRole("Admin"))
            {
                    return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,product_id,account_id,order_delivery,order_price,product_name")] iOrders iOrders)
        {
            if (Request.IsAuthenticated && this.User.IsInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    db.iOrders.Add(iOrders);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(iOrders);
            }
            else
            {
                return Redirect("/");
            }

        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iOrders iOrders = db.iOrders.Find(id);
            if (iOrders == null)
            {
                return HttpNotFound();
            }
            return View(iOrders);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,product_id,account_id,order_delivery,order_price,product_name")] iOrders iOrders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iOrders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iOrders);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iOrders iOrders = db.iOrders.Find(id);
            if (iOrders == null)
            {
                return HttpNotFound();
            }
            return View(iOrders);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            iOrders iOrders = db.iOrders.Find(id);
            db.iOrders.Remove(iOrders);
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
