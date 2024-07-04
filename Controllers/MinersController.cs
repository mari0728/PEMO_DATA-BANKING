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
            var miners = db.Miners.Include(m => m.Association);
            return View(miners.ToList());
        }


        public ActionResult Create()
        {
            ViewBag.Association_id = new SelectList(db.Associations, "Association_id", "Association_name");
            return PartialView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Association_id,FirstName,MiddleName,LastName,Longitude,Latitude")] Miner miner)
        {
            if (ModelState.IsValid)
            {
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
            db.Miners.Remove(miner);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
