using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CRT;
using Microsoft.AspNet.Identity;

namespace CRT.Controllers
{
    public class ProductsController : Controller
    {
        private DataEntities db = new DataEntities();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.iProducts.ToList());
        }

        public async Task<ActionResult> OrderSearch(string searchString)
        {
            return View(await db.iProducts.Where(p => p.Name.Contains(searchString)).ToListAsync());
        }

        public ActionResult addIntoCart(int productId)
        {
            var eprod = db.iProducts.Find(productId);

            if (eprod != null)
            {
                iCarts basket = new iCarts();

                basket.product_id = productId;
                basket.account_id = User.Identity.GetUserId();
                basket.product_name = eprod.Name;
                basket.product_descript = eprod.Description;
                basket.product_price = eprod.Price;
                basket.product_qua = 1;
                basket.product_img = eprod.Img;

                db.iCarts.Add(basket);
                db.SaveChanges();
            }

            return Redirect("/");
        }


        // GET: Products/Details/5
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

        // GET: Products/Edit/5
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

        // POST: Products/Edit/5
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

        // GET: Products/Delete/5
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

        // POST: Products/Delete/5
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
