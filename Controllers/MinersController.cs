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
                           .Where(m => m.Status == "Created")
                           .ToList();
            ViewBag.Profile = "Miner";
            return View(miners);
        }


        public ActionResult Create()
        {
            ViewBag.Association_id = new SelectList(db.Associations, "Association_id", "Association_name");
            return PartialView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Association_id,FirstName,MiddleName,LastName,Longitude,Latitude,DateCreated,DateDeleted,Status")] Miner miner)
        {
            // Check for existing data
            var existingMiner = db.Miners.FirstOrDefault(m => m.FirstName == miner.FirstName && m.MiddleName == miner.MiddleName && m.LastName == miner.LastName);

            if (existingMiner != null)
            {
                // Set error message
                TempData["ErrorMessage"] = "Data already exists.";
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                miner.DateCreated = DateTime.Now; // Set current date/time
                miner.Status = "Created"; // Set status to "Created"
                db.Miners.Add(miner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Association_id = new SelectList(db.Associations, "Association_id", "Association_name", miner.Association_id);
            return PartialView("Create", miner);
        }




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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Miner_id,Association_id,FirstName,MiddleName,LastName,Longitude,Latitude")] Miner miner)
        {
            if (ModelState.IsValid)
            {
                miner.DateCreated = miner.DateCreated; // Set current date/time
                miner.Status = "Created"; // Set status to "Created"
                db.Entry(miner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Association_id = new SelectList(db.Associations, "Association_id", "Association_name", miner.Association_id);
            return PartialView("Edit", miner);
        }

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Miner miner = db.Miners.Find(id);
            if (miner == null)
            {
                return HttpNotFound();
            }

            // Update status to indicate deleted
            miner.DateDeleted = DateTime.Now;
            miner.Status = "Deleted";

            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
