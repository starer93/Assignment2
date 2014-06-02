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
    [Authorize]
    public class ExpenseController : Controller
    {
        public IReportDataAccess reportDataAccess = new SqlReportDataAccess();

        // GET: /Expense/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Expense/Create
        //adding expenses to the temporary currentExpense list
        [HttpPost]
        public ActionResult Create(Expense expense, string currency)
        {
            //temporary holder for expenses
            List<Expense> currentExpenses = new List<Expense>();

            if (ModelState.IsValid)
            {
                //if any reports exist, get last id and add 1
                expense.ReportId = SetReportId(expense);

                switch (currency)
                {
                    case "CNY" :
                        expense.Amount *= 0.17;
                        break;
                    case "EUR" :
                        expense.Amount *= 1.47;
                        break;
                    default :
                        break;
                }

                //if expense list new, store in session
                if (Session["CurrentExpenses"] == null)
                {
                    currentExpenses.Add(expense);
                }
                else
                {
                    //add to currentExpense list
                    currentExpenses = (List<Expense>)Session["CurrentExpenses"];
                    currentExpenses.Add(expense);
                }

                Session["CurrentExpenses"] = currentExpenses;
                
                return RedirectToAction("Create", "Report");
            }
            else
            {
                //need message to tell user expense was not added successfully
                //missing fields, incorrectly entered fields etc.
                //Session["CurrentExpenses"] = null;
                return RedirectToAction("Create", "Expense");
            }

            //reportDataAccess.AddExpense(expense);
            //reportDataAccess.SaveChanges();
        }

        private int SetReportId(Expense expense)
        {
            if (reportDataAccess.GetAllReports().Count() > 0)
            {
                Report report = reportDataAccess.GetAllReports().Last();
                return report.Id + 1;
            }
            else
            {
                //otherwise this is the first report
                return 1;
            }
        }

    }
}