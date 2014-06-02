using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment_2.Models;
using Assignment_2.Business_Logic;

namespace Assignment_2.Controllers
{
    [Authorize (Roles="Account Staff")]
    public class AccountStaffController : Controller
    {
        // GET: /AccountStaff/
        UsersContext userContext = new UsersContext();
        public IReportDataAccess reportDataAccess = new SqlReportDataAccess();
        private AccountStaffLogic accountStaff = new AccountStaffLogic();

        public AccountStaffController(IReportDataAccess dataAccess)
        {
            reportDataAccess = dataAccess;
        }

        public AccountStaffController()
        {

        }

        public ActionResult Approve(int id)
        {
            accountStaff.changeReportStatus(id, "ApprovedByAccountStaff");
            return RedirectToAction("ViewReports");
        }

        public ActionResult Reject(int id)
        {
            accountStaff.changeReportStatus(id, "RejectedByAccountStaff");
            return RedirectToAction("ViewReports");
        }

        public ActionResult ViewReports()
        {
            List<Report> reportList = reportDataAccess.GetAllReports();
            List<Report> filteredReports = new List<Report>();

            //filter reports by consultant name
            foreach (Report report in reportList)
            {
                if (report.Status.Equals("ApprovedByDepartmentSupervisor"))
                {
                    filteredReports.Add(report);
                }
            }
            return View(filteredReports);
        }


        [HttpGet]
        public ActionResult Index()
        {
            List<Supervisor> supervisors = new List<Supervisor>();
            foreach(UserProfile user in userContext.UserProfiles)
            {
                if(user.Role.Equals("Department Supervisor"))
                {
                    supervisors.Add(new Supervisor());
                    supervisors.Last().UserName = user.UserName;
                }
            }
            List<Report> reports = reportDataAccess.GetAllReports();
            foreach (Supervisor supervisor in supervisors)
            {
                double sum = 0;
                foreach (Report report in reports)
                {
                    if (report.ApprovedBy == supervisor.UserName && approved(report))
                    {
                        ReportLogic reportLogic = new ReportLogic(report);
                        sum += reportLogic.calculateExpenses();
                    }
                }
                supervisor.ApprovedBudget = sum;
            }

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

        private bool approved(Report report)
        {
            return report.Status.Equals("ApprovedByAccountStaff") || report.Status.Equals("ApprovedByDepartmentSupervisor");
        }
    }
}
