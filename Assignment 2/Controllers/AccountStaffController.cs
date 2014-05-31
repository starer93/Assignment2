using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment_2.Models;
using Assignment_2.Business_Logic;

namespace Assignment_2.Controllers
{
    public class AccountStaffController : Controller
    {
        // GET: /AccountStaff/
        UsersContext userContext = new UsersContext();
        SqlReportDataAccess dataAcesss = new SqlReportDataAccess();

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult CompanyDetail()
        {
            List<Supervisor> supervisors = new List<Supervisor>();
            List<Report> reports = dataAcesss.GetAllReports();
            foreach (Supervisor supervisor in supervisors)
            {
                foreach (Report report in reports)
                {
                    if (report.ApprovedBy == supervisor.UserName)
                    {
                        ReportLogic reportLogic = new ReportLogic(report);
                        double sum = reportLogic.calculateExpenses();
                        supervisor.ApprovedBudget = sum;

                    }
                }
            }
            AccountStaffLogic accountStaff = new AccountStaffLogic();
            List<double> departmentBudget = accountStaff.getDepartmentBudget();
            for (int i = 0; i < departmentBudget.Count; i++)
            {
                switch (i)
                {
                    case 0: ViewBag.StateServices = departmentBudget[i]; break;
                    case 1: ViewBag.LogisticsServices = departmentBudget[i]; break;
                    case 2: ViewBag.HigherEducationServices = departmentBudget[i]; break;
                    default: ViewBag.LogisticsServices = "error"; break;
                }
            }
            return View(supervisors);
        }

    }
}
