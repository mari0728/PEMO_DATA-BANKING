using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PEMO_DATA_BANKING.Models;

namespace PEMO_DATA_BANKING.Controllers
{
    public class MinersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Miners
        public ActionResult Index()
        {
            object miners1 = db.Miners;
            var miners = miners1.Include(m => m.Association).ToList();
            return View(miners);
        }

        //// GET: Miners/Details/5
        //public ActionResult Details(int id)
        //{
        //    var miner = db.Miners.Include(m => m.Association).FirstOrDefault(m => m.Miner_id == id);
        //    if (miner == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(miner);
        //}

        //// GET: Miners/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Miners/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "FirstName,MiddleName,LastName,Longitude,Latitude,AssociationId")] Miner miner)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Miners.Add(miner);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch
        //    {
        //        // Handle the error (log it, rethrow it, etc.)
        //    }
        //    return View(miner);
        //}

        //// GET: Miners/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    var miner = db.Miners.Find(id);
        //    if (miner == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(miner);
        //}

        //// POST: Miners/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Miner_id,FirstName,MiddleName,LastName,Longitude,Latitude,AssociationId")] Miner miner)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Entry(miner).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch
        //    {
        //        // Handle the error (log it, rethrow it, etc.)
        //    }
        //    return View(miner);
        //}

        //// GET: Miners/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    var miner = db.Miners.Find(id);
        //    if (miner == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(miner);
        //}

        //// POST: Miners/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        var miner = db.Miners.Find(id);
        //        db.Miners.Remove(miner);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        // Handle the error (log it, rethrow it, etc.)
        //    }
        //    return View();
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
