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
        public IReportDataAccess reportDataAccess = new SqlReportDataAccess();

        public ConsultantController(IReportDataAccess dataAccess)
        {
            reportDataAccess = dataAccess;
        }

        public ConsultantController()
        {
        }

        //
        // GET: /Consultant/Index
        public ActionResult Index()
        {
            CheckDataBase(); // not sure this is working well

            List<Report> reportList = reportDataAccess.GetAllReports();
            List<Report> filteredReports = new List<Report>();

            //filter reports by consultant name
            foreach (Report report in reportList)
            {
                if (report.ConsultantId.Equals(User.Identity.Name))
                {
                    filteredReports.Add(report);
                }
            }
            return View(filteredReports);
        }

        //clears out spare expenses that aren't attached to any report
        private void CheckDataBase()
        {
            List<Expense> expenses = reportDataAccess.GetAllExpenses();
            foreach (Expense expense in expenses)
            {
                if (reportDataAccess.FindReportByPrimaryKey(expense.ReportId) == null)
                {
                    reportDataAccess.RemoveExpense(expense);
                }
            }
            //reportDataAccess.Dispose();
        }

        //this method should be the last method called before submitting a report
        //i.e. after attaching a receipt, not sure this method is in the right place...
        //Create, Consultant
        [HttpPost]
        public ActionResult Create(Report report, HttpPostedFileBase file)
        {
            //initialising report data
            report.ConsultantId = User.Identity.Name;
            report.Date = DateTime.Today.ToString("dd/MM/yyyy");
            report.Status = "SubmittedByConsultant";

            List<Expense> currentExpenses = (List<Expense>)Session["CurrentExpenses"];

            //checking current expense session
            if (currentExpenses != null)
            {
                report.Expenses = currentExpenses;
            }
            //else
            //{
            //    //no expenses exist, do not continue 
            //    return RedirectToAction("Index");
            //}

            //checking receipt upload
            if (file.ContentLength > 0)
            {
                int length = file.ContentLength;
                byte[] fileData = new byte[length];
                file.InputStream.Read(fileData, 0, length);
                report.Receipt = fileData;
            }

            //save to db if all report fields have valid values
            if (ModelState.IsValid)
            {
                reportDataAccess.AddReport(report);
                reportDataAccess.AddAllExpenses(currentExpenses);
                reportDataAccess.SaveChanges();
                //does this method save expenses?
            }

            //empty the session
            Session["CurrentExpenses"] = null;
            return RedirectToAction("Index");
        }

        
        //public ActionResult Details(int id)
        //{
        //    return RedirectToAction("Details", "Report", id);
        //}
    }
}
