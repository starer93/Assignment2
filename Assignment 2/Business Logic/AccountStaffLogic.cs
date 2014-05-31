using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment_2.Models;

namespace Assignment_2.Business_Logic
{
    public class AccountStaffLogic
    {
        public Report ReportModel { get; private set; }

        public AccountStaffLogic(Report report)
        {
            ReportModel = report;
        }
        public AccountStaffLogic()
        {
        }

        public List<double> getDepartmentBudget()
        {
            List<double> budgets = new List<double>();
            List<string> departments = new List<string>();
            departments.Add("State Services");
            departments.Add("Logisics Services");
            departments.Add("Higher Education Services");
            foreach(string department in departments)
            {
                DepartmentLogic departmentLogic= new DepartmentLogic(department);
                budgets.Add(departmentLogic.getRemainingBudget());
            }
            return budgets;
        }
        
    
    }
}