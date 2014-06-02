using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment_2.Business_Logic;
using Assignment_2.Models;

namespace Assignment_2.Controllers
{
    [Authorize(Roles = "Department Supervisor")]
    public class DepartmentSupervisorController : Controller
    {
        public IReportDataAccess reportDataAccess;
        public ActionResult Index()
        {
            DepartmentSupervisorLogic departmentSupervisor = new DepartmentSupervisorLogic(User.Identity.Name);
            ViewBag.Username = User.Identity.Name;
            ViewBag.Department = departmentSupervisor.Department;
            Session["Department Supervisor"] = departmentSupervisor;
            List<Report> reports = departmentSupervisor.Department.getDepartmentReports();
            return View(reports);
        }

        public ActionResult Approve(int id)
        {
            DepartmentSupervisorLogic departmentSupervisor = (DepartmentSupervisorLogic)Session["Department Supervisor"];
            departmentSupervisor.changeReportStatus(id, "ApprovedByDepartmentSupervisor");  
            return RedirectToAction("Index");
        }

        public ActionResult Reject(int id)
        {
            DepartmentSupervisorLogic departmentSupervisor = (DepartmentSupervisorLogic)Session["Department Supervisor"];
            departmentSupervisor.changeReportStatus(id, "RejectedByDepartmentSupervisor");
            return RedirectToAction("Index");
        }

        public ActionResult FilterReport(string status)
        {
            DepartmentSupervisorLogic departmentSupervisor = (DepartmentSupervisorLogic)Session["Department Supervisor"];
            List<Report> reports = departmentSupervisor.Department.getDepartmentReports(status);
            ViewBag.Username = User.Identity.Name;
            ViewBag.Department = departmentSupervisor.Department;
            return View("Index", reports);
        }
    }
}
