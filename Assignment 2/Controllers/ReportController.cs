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
        public IReportDataAccess reportDataAccess = new SqlReportDataAccess();

        public ReportController(IReportDataAccess dataAccess)
        {
            reportDataAccess = dataAccess;
        }
        public ReportController()
        {

        }

        public ActionResult ReturnToCorrectIndex()
        {
            if (User.IsInRole("Consultant"))
            {
                return RedirectToAction("Index", "Consultant");
            }
            else if (User.IsInRole("Department Supervisor"))
            {
                return RedirectToAction("Index", "DepartmentSupervisor");
            }
            else if (User.IsInRole("Account Staff"))
            {
                return RedirectToAction("Index", "AccountStaff");
            }
            else
            {
                return View();
            }

        }

        //
        // GET: /Report/

        public ActionResult Index()
        {
            List<Report> reportList = reportDataAccess.GetAllReports();
            List<Report> filteredReports = new List<Report>();

            foreach (Report report in reportList)
            {
                if (report.ConsultantId.Equals(User.Identity.Name))
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
            Report report = reportDataAccess.FindReportByPrimaryKey(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        public ActionResult ShowReceipt()
        {
            return View();
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
                reportDataAccess.AddReport(report);
                reportDataAccess.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(report);
        }

        //
        // GET: /Report/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Report report = reportDataAccess.FindReportByPrimaryKey(id);
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
                reportDataAccess.ChangeState(report, EntityState.Modified);
                reportDataAccess.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(report);
        }

        //
        // GET: /Report/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Report report = reportDataAccess.FindReportByPrimaryKey(id);
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
            Report report = reportDataAccess.FindReportByPrimaryKey(id);
            reportDataAccess.RemoveReport(report);
            reportDataAccess.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            reportDataAccess.Dispose();
            base.Dispose(disposing);
        }
    }
}