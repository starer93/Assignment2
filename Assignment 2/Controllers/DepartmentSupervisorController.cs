using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment_2.Business_Logic;
using Assignment_2.Models;

namespace Assignment_2.Controllers
{
    public class DepartmentSupervisorController : Controller
    {
        DepartmentSupervisorLogic departmentSupervisor;

        [Authorize(Roles= "Department Supervisor")]
        public ActionResult Index()
        {
            departmentSupervisor = new DepartmentSupervisorLogic(User.Identity.Name);

            
            ViewBag.Username = User.Identity.Name;
            ViewBag.DepartmentName = departmentSupervisor.Department.Name;
            ViewBag.DepartmentBudget = departmentSupervisor.Department.getTotalBudget();
            ViewBag.RemainingBudget = departmentSupervisor.Department.getRemainingBudget();
            ViewBag.test = departmentSupervisor.Department.Reports.First().ReportModel.Id;
            ViewBag.ExpenseReportApproved = 0;
            return View();
        }

    }
}
