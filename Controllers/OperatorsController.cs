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
    public class OperatorsController : Controller
    {
        private PEMOEntities db = new PEMOEntities();

        // GET: Operators
        public ActionResult Index()
        {
            ViewBag.Profile = "Operator";
            var activeOperators = db.Operators.Where(o => o.Status == "Active").ToList();
            return View(activeOperators);
        }

        // GET: Operators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operator @operator = db.Operators.Find(id);
            if (@operator == null)
            {
                return HttpNotFound();
            }
            return View(@operator);
        }


        // GET: Operators/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: Operators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operator @operator = db.Operators.Find(id);
            if (@operator == null)
            {
                return HttpNotFound();
            }
            return PartialView("Edit", @operator);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Person_id,FirstName,MiddleName,LastName,isCompany,Company_Name")] Operator @operator)
        {
            if (ModelState.IsValid)
            {
                @operator.isCompany = @operator.isCompany == "Yes" ? "Yes" : "No"; // Convert "Yes"/"No" back to bool if needed

                db.Entry(@operator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView("Edit", @operator);
        }

        // GET: Operators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operator @operator = db.Operators.Find(id);
            if (@operator == null)
            {
                return HttpNotFound();
            }
            return PartialView("Delete", @operator);
        }

        // POST: Operators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Operator @operator = db.Operators.Find(id);
            if (@operator == null)
            {
                return HttpNotFound();
            }

            // Update status to indicate deleted
            @operator.DateDeleted = DateTime.Now;
            @operator.Status = "Deleted";

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertData(string FirstName, string MiddleName, string LastName, string isCompany, string Company_Name)
        {
            if (ModelState.IsValid)
            {
                // Creating a new Operator object with the provided data
                Operator newOperator = new Operator
                {
                    FirstName = FirstName,
                    MiddleName = MiddleName,
                    LastName = LastName,
                    isCompany = isCompany,
                    Company_Name = Company_Name,
                    DateCreated = DateTime.Now,  // Set the current date for DateCreated
                    Status = "Active"  // Assuming a default status of "Active"
                };

                db.Operators.Add(newOperator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
