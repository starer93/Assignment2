using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment_2.Models;
using Assignment_2.SAL;

namespace Assignment_2.Controllers
{
    public class ReportController : Controller
    {
        private ReportContext db = new ReportContext();

        //
        // GET: /Report/

        public ActionResult Index()
        {
            List<Report> reportList = db.Reports.ToList();
            List<Report> filteredReports = new List<Report>();
            
            foreach (Report report in reportList)
            {
                if (report.ConsultantID.Equals(User.Identity.Name))
                {
                    filteredReports.Add(report);
                }
            }
            return View(filteredReports);
        }

        //
        // GET: /Report/Details/5

        public ActionResult Details(int id = 0)
        {
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        //
        // GET: /Report/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Report/Create

        [HttpPost]
        public ActionResult Create(Report report)
        {
            if (ModelState.IsValid)
            {
                db.Reports.Add(report);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(report);
        }

        //
        // GET: /Report/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        //
        // POST: /Report/Edit/5

        [HttpPost]
        public ActionResult Edit(Report report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(report);
        }

        //
        // GET: /Report/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        //
        // POST: /Report/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Report report = db.Reports.Find(id);
            db.Reports.Remove(report);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}