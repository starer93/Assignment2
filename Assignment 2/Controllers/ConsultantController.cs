using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Assignment_2.Models;
using Assignment_2.SAL;

namespace Assignment_2.Controllers
{
    [Authorize(Roles = "Consultant")]
    public class ConsultantController : Controller
    {
        //
        // GET: /Consultant/
        public IReportDataAccess reportDataAccess = new SqlReportDataAccess();

        public ConsultantController(IReportDataAccess dataAccess)
        {
            reportDataAccess = dataAccess;
        }

        public ConsultantController()
        {
        }


        
        //public ActionResult Index()
        //{
        //    return View();
        //}

        
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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Report report, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                reportDataAccess.AddReport(report);
                reportDataAccess.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(report);
        }
    }
}
