using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment_2.Models;
using Assignment_2.Controllers;
namespace Assignment_2.Business_Logic
{
    public class DepartmentSupervisorLogic
    {
        public string Username { get; private set; }
        public DepartmentLogic Department { get; private set; }
        public IReportDataAccess reportDataAccess;
        public DepartmentSupervisorLogic(string username)
        {
            this.Username = username;
            loadDepartment();
            reportDataAccess = new SqlReportDataAccess();
        }
        public DepartmentSupervisorLogic(string username, MockReportDataAccess mockReportDataAccess)
        {
            this.Username = username;
            reportDataAccess = mockReportDataAccess;
        }
        private void loadDepartment()
        {
            UsersContext userContext = new UsersContext();
            var departmentName = from user in userContext.UserProfiles
                             where user.UserName == this.Username
                             select user.Department;
            Department = new DepartmentLogic(departmentName.Single());
        }

        public void changeReportStatus(int Id, string status)
        {
            foreach (Report report in reportDataAccess.GetAllReports())
            {
                if (report.Id == Id)
                {
                    report.Status = status;
                    report.ApprovedBy = Username;
                    reportDataAccess.SaveChanges();
                }
            }
        }

    }
}