using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Assignment_2.SAL;
using Assignment_2.Models;


namespace Assignment_2.Controllers
{
    public class ConsultantController : Controller
    {
        private ReportContext db = new ReportContext();

        [Authorize(Roles="Consultant")]
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

        public ActionResult Details(int id = 0)
        {
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        public ActionResult CreateReport()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateReport(Report report)
        {
            if (ModelState.IsValid)
            {
                db.Reports.Add(report);
                db.SaveChanges();
                
                return RedirectToAction("Index", report.Id);
            }

            return View(report);
        }

        [HttpPost]
        public ActionResult CreateExpense(Expense expense, int reportId)
        {
            if (ModelState.IsValid)
            {
                expense.ReportId = reportId;
                db.Expenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("CreateReport");
            }
            return View();
        }

        public ActionResult CreateExpense(Expense expense)
        {
            return View(expense);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
