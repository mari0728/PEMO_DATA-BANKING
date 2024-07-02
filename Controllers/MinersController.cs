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

        // GET: Miners/Details/5
        public ActionResult Details(int? id)
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
            return View(miner);
        }

        // GET: Miners/Create
        public ActionResult Create()
        {
            ViewBag.Association_id = new SelectList(db.Associations, "Association_id", "Association_name");
            return View();
        }

        // POST: Miners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Miner_id,Association_id,FirstName,MiddleName,LastName,Longitude,Latitude")] Miner miner)
        {
            if (ModelState.IsValid)
            {
                db.Miners.Add(miner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Association_id = new SelectList(db.Associations, "Association_id", "Association_name", miner.Association_id);
            return View(miner);
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
            return View(miner);
        }

        // POST: Miners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
            return View(miner);
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
            return View(miner);
        }

        // POST: Miners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Miner miner = db.Miners.Find(id);
            db.Miners.Remove(miner);
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
