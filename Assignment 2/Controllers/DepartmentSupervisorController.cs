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
        DepartmentSupervisorLogic departmentSupervisor;

        public ActionResult Index()
        {
            departmentSupervisor = new DepartmentSupervisorLogic(User.Identity.Name);
            ViewBag.Username = User.Identity.Name;
            ViewBag.Department = departmentSupervisor.Department;
            return View();
        }

    }
}
