using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment_2.Models;
using Assignment_2.Controllers;
namespace Assignment_2.Business_Logic
{
    public class AccountStaffLogic
    {
        public Report ReportModel { get; private set; }
        public IReportDataAccess reportDataAccess;
        


        public AccountStaffLogic()
        {
            //
            reportDataAccess = new SqlReportDataAccess();
        }

        #region Mocklogic

        public AccountStaffLogic(Report report, MockReportDataAccess mockDataAccess)
        {
            ReportModel = report;
            reportDataAccess = mockDataAccess;
        }
        public AccountStaffLogic(MockReportDataAccess mockDataAccess)
        {
            reportDataAccess = mockDataAccess;
        }

        #endregion

        public List<double> getDepartmentBudget()
        {
            List<double> budgets = new List<double>();
            List<string> departments = new List<string>();
            departments.Add("State Services");
            departments.Add("Logisics Services");
            departments.Add("Higher Education Services");
            foreach(string department in departments)
            {
                DepartmentLogic departmentLogic;
                if (reportDataAccess.IsMock())
                    departmentLogic = new DepartmentLogic(department, reportDataAccess);
                else
                    departmentLogic = new DepartmentLogic(department); //If the mock constructor was called, call the next mock constructor
                budgets.Add(departmentLogic.getRemainingBudget());
            }
            return budgets;
        }

        public void changeReportStatus(int Id, string status)
        {
            foreach (Report report in reportDataAccess.GetAllReports())
            {
                if (report.Id == Id)
                {
                    report.Status = status;
                    reportDataAccess.SaveChanges();
                }
            }
        }


    }
}