using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PEMO_DATA_BANKING.Models;

namespace PEMO_DATA_BANKING.Controllers
{
    public class MinersController : Controller
    {
        private PEMOEntities db = new PEMOEntities();

        // GET: Miners
        public ActionResult Index()
        {
            var miners = db.Miners
                           .Include(m => m.Association)
                           .Where(m => m.Status == "Active")
                           .ToList();
            ViewBag.Profile = "Miner";
            ViewBag.Associations = new SelectList(db.Associations.Where(m => m.Status == "Active"), "Association_id", "Association_name");
            return View(miners);
        }

        // GET: Miners/Create
        public ActionResult Create()
        {
            ViewBag.Association_id = new SelectList(db.Associations, "Association_id", "Association_name");
            return PartialView("Create");
        }

        // POST: Miners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Association_id,FirstName,MiddleName,LastName,Longitude,Latitude,DateCreated,DateDeleted,Status")] Miner miner)
        {
            // Check for existing data
            var existingMiner = db.Miners.FirstOrDefault(m => m.FirstName == miner.FirstName && m.MiddleName == miner.MiddleName && m.LastName == miner.LastName);

            if (existingMiner != null)
            {
                TempData["ErrorMessage"] = "Data already exists.";
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                miner.DateCreated = DateTime.Now; // Set current date/time
                miner.Status = "Active"; // Set status to "Active"
                db.Miners.Add(miner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Association_id = new SelectList(db.Associations, "Association_id", "Association_name", miner.Association_id);
            return PartialView("Create", miner);
        }

        // GET: Miners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Miner miner = db.Miners.Find(id);
            if (miner == null)
            {
                return HttpNotFound();
            }
            ViewBag.Association_id = new SelectList(db.Associations, "Association_id", "Association_name", miner.Association_id);
            return PartialView("Edit", miner);
        }

        // POST: Miners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Miner_id,Association_id,FirstName,MiddleName,LastName,Longitude,Latitude")] Miner miner)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    miner.DateCreated = DateTime.Now;
                    miner.Status = "Active";
                    db.Entry(miner).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["EditSuccess"] = true;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {

                throw;
            }
            
            ViewBag.Association_id = new SelectList(db.Associations, "Association_id", "Association_name", miner.Association_id);
            return PartialView("Edit", miner);
        }

        // GET: Miners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Miner miner = db.Miners.Find(id);
            if (miner == null)
            {
                return HttpNotFound();
            }
            return PartialView("Delete", miner);
        }

        // POST: Miners/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Miner miner = db.Miners.Find(id);
                if (miner == null)
                {
                    return Json(new { success = false, message = "Miner not found." });
                }

                // Update status to indicate deleted
                miner.DateDeleted = DateTime.Now;
                miner.Status = "Deleted";
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return Json(new { success = false, message = "An error occurred while deleting the miner." });
            }
        }

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
