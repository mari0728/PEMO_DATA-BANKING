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

        // GET: Operators/Create
        public ActionResult Create()
        {
            return View();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditData(int Person_id, string FirstName, string MiddleName, string LastName, string isCompany, string Company_Name)
        {
            if (ModelState.IsValid)
            {
                Operator @operator = db.Operators.Find(Person_id);
                if (@operator == null)
                {
                    return HttpNotFound();
                }

                @operator.FirstName = FirstName;
                @operator.MiddleName = MiddleName;
                @operator.LastName = LastName;
                @operator.isCompany = isCompany;
                @operator.Company_Name = Company_Name;

                db.Entry(@operator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteData(int Person_id)
        {
            Operator @operator = db.Operators.Find(Person_id);
            if (@operator == null)
            {
                return HttpNotFound();
            }

            @operator.DateDeleted = DateTime.Now;
            @operator.Status = "Deleted";

            db.Entry(@operator).State = EntityState.Modified;
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
