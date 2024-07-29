using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PEMO_DATA_BANKING.Models;

namespace PEMO_DATA_BANKING.Controllers
{
    public class AssociationController : Controller
    {
        private PEMOEntities db = new PEMOEntities();

        // GET: Association
        public ActionResult Index()
        {
            var associations = db.Associations
                                 .Where(m => m.Status == "Active")
                                 .ToList();
            ViewBag.Profile = "Association";
            return View(associations);
        }

        // GET: Association/Create
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        // POST: Association/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Association_name")] Association association)
        {
            var existingAssociation = db.Associations
                                        .FirstOrDefault(m => m.Association_name == association.Association_name);

            if (existingAssociation != null)
            {
                TempData["ErrorMessage"] = "Data already exists.";
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                association.DateCreated = DateTime.Now;
                association.Status = "Active";
                db.Associations.Add(association);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return PartialView("Create", association);
        }

        // GET: Association/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Association association = db.Associations.Find(id);
            if (association == null)
            {
                return HttpNotFound();
            }
            return PartialView("Edit", association);
        }

        // POST: Association/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Association_id,Association_name")] Association association)
        {
            if (ModelState.IsValid)
            {
                association.DateCreated = association.DateCreated; // Preserve original DateCreated
                association.Status = "Active";
                db.Entry(association).State = EntityState.Modified;
                db.SaveChanges();
                TempData["EditSuccess"] = true;
                return RedirectToAction("Index");
            }
            return PartialView("Edit", association);
        }

        // GET: Association/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Association association = db.Associations.Find(id);
            if (association == null)
            {
                return HttpNotFound();
            }
            return PartialView("Delete", association);
        }

        // POST: Association/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Association association = db.Associations.Find(id);
            if (association == null)
            {
                return HttpNotFound();
            }

            // Update status to indicate deleted
            association.DateDeleted = DateTime.Now;
            association.Status = "Deleted";
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
