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
        #region DataAccess
        public IReportDataAccess reportDataAccess = new SqlReportDataAccess();

        public interface IReportDataAccess
        {

            List<Report> GetAllReports();
            Report FindReportByPrimaryKey(int id);
            void AddReport(Report report);
            void SaveChanges();
            void ChangeState(Report report, EntityState state);
            void RemoveReport(Report report);
            void Dispose();
        }

        public class MockReportDataAccess : IReportDataAccess
        {
            private List<Report> reports;

            public MockReportDataAccess()
            {
                reports = new List<Report>();
            }

            public List<Report> GetAllReports()
            {
                return reports;
            }

            public Report FindReportByPrimaryKey(int id)
            {
                return reports.FirstOrDefault(x => x.Id == id);

                //Report report = new Report();
                //report.Id = 15;
                //if (id == 15)
                //{
                //    return report;
                //}
                //else
                //    return null;
            }

            public void AddReport(Report report)
            {
                reports.Add(report);
            }

            public void SaveChanges()
            {
                //db.SaveChanges();
            }

            public void ChangeState(Report report, EntityState state)
            {
                //db.Entry(report).State = state;
            }

            public void RemoveReport(Report report)
            {
                //db.Reports.Remove(report);
            }

            public void Dispose()
            {
                //db.Dispose();
            }
        }

        public class SqlReportDataAccess : IReportDataAccess
        {
            ReportContext db = new ReportContext();

            public List<Report> GetAllReports()
            {
                return db.Reports.ToList();
            }
            public Report FindReportByPrimaryKey(int id)
            {
                return db.Reports.Find(id);
            }
            public void AddReport(Report report)
            {
                db.Reports.Add(report);
            }
            public void SaveChanges()
            {
                db.SaveChanges();
            }
            public void ChangeState(Report report, EntityState state)
            {
                db.Entry(report).State = state;
            }
            public void RemoveReport(Report report)
            {
                db.Reports.Remove(report);
            }
            public void Dispose()
            {
                db.Dispose();
            }
        }
        #endregion
        public ReportController(IReportDataAccess dataAccess)
        {
            reportDataAccess = dataAccess;
        }
        public ReportController()
        {

        }
        //
        // GET: /Report/

        public ActionResult Index()
        {
            List<Report> reportList = reportDataAccess.GetAllReports();
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
            Report report = reportDataAccess.FindReportByPrimaryKey(id);
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