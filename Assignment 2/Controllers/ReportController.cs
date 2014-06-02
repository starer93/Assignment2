using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment_2.Models;
using Assignment_2.SAL;
using Assignment_2.Business_Logic;

namespace Assignment_2.Controllers
{
    [Authorize]
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

        //GET: Report/Create
        //1. show a list of the current expenses in new report
        public ActionResult Create()
        {
            List<Expense> currentExpenses = new List<Expense>();
            currentExpenses = (List<Expense>)Session["CurrentExpenses"];
            return View(currentExpenses);
        }

        // POST:
        //2. once weve added all the expenses we want, we build the rest of the report
        [HttpPost]
        public ActionResult Create(Report report)
        {
            report.ConsultantId = User.Identity.Name;
            report.Date = DateTime.Today.ToString("dd/MM/yyyy");
            report.Status = "SubmittedByConsultant";
            report.Expenses = (List<Expense>)Session["CurrentExpenses"];

            if (ModelState.IsValid)
            {
                reportDataAccess.AddReport(report);
                return RedirectToAction("AttachReceipt");
            }

            return View(report);
        }

        //GET: /Report/AttachReceipt
        //3. show file upload control for uploading receipts
        public ActionResult AttachReceipt()
        {
            return View();
        }

        // OTHER METHODS ===============================

        // GET: /Report/Details/5
        //(a) get the report details of the selected report
        public ActionResult Details(int id)
        {
            Report report = reportDataAccess.FindReportByPrimaryKey(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            
            if (User!=null&&User.IsInRole("Department Supervisor"))
            {
                DepartmentSupervisorLogic departmentSupervisor = new DepartmentSupervisorLogic(User.Identity.Name);
                ViewBag.IsOverBudget = departmentSupervisor.Department.willBeOverBudget(new ReportLogic(report));
            }
            //checkoverbudget
            return View(report);
        }

        //(c)used by all users if they want to return to their base index
        public ActionResult ReturnToCorrectIndex()
        {
            Session["CurrentExpenses"] = null;

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
                //unreachable code
                return View();
            }
        }

        //(d)why?
        protected override void Dispose(bool disposing)
        {
            reportDataAccess.Dispose();
            base.Dispose(disposing);
        }

        [HttpGet]
        public FileStreamResult ViewReceipt(int id)
        {
            MemoryStream ms = new MemoryStream();

            byte[] pdf = reportDataAccess.FindReportByPrimaryKey(id).Receipt;
            ms.Write(pdf, 0, pdf.Length);
            ms.Position = 0;
            Stream fileStream = ms;
            HttpContext.Response.AddHeader("content-disposition",
            "attachment; filename=Receipt.pdf");

            return new FileStreamResult(fileStream, "application/pdf");
        }

    }
}