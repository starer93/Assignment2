using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment_2.SAL;
using Assignment_2.Models;
using Assignment_2.Controllers;

namespace Assignment_2.Business_Logic
{
    public class DepartmentLogic
    {
        public string Name { get; private set; }
        private const double MONTHLY_BUDGET = 10000;
        private double remainingBudget = 0;
        public List<ReportLogic> Reports { get; private set; }
        private IReportDataAccess reportDataAccess;

        public DepartmentLogic(string departmentName)
        {
            Reports = new List<ReportLogic>();
            Name = departmentName;
            reportDataAccess = new SqlReportDataAccess();
            UsersContext usersContext = new UsersContext();
            List<UserProfile> users = usersContext.UserProfiles.ToList();
            loadReports(users);
        }

        #region MockLogic

        public DepartmentLogic(string departmentName, IReportDataAccess reportDataAccess)
        {         
            Reports = new List<ReportLogic>();
            Name = departmentName;
            List<UserProfile> users = new List<UserProfile>();
            UserProfile user = new UserProfile();
            user.Department = "Higher Education Services";
            user.Role = "Consultant";
            user.UserId = 1;
            user.UserName = "125";
            this.reportDataAccess = reportDataAccess;
            users.Add(user);
            loadReports(users);
        }

        #endregion

        private void loadReports(List<UserProfile> users)
        {
            List<Report> reportModels = reportDataAccess.GetAllReports();
            foreach (Report report in reportModels)
            {
                string consultantID = report.ConsultantId;
                foreach (UserProfile userProfile in users)
                {
                    if (consultantID.Equals(userProfile.UserName))
                    {
                        if (userProfile.Department.Equals(Name))
                        {
                            Reports.Add(new ReportLogic(report));
                        }
                    }
                }
            }
        }
    

        //?????
        public double getTotalBudget()
        {
            return MONTHLY_BUDGET;
        }

        //??????
        public double TotalExpense()
        {
            double sum = 0;
            foreach (ReportLogic report in Reports)
            {
                if (IsApproved(report))
                {
                    sum += report.calculateExpenses();
                }
            }
            return sum;
        }

        private bool IsApproved(ReportLogic report)
        {
            return report.ReportModel.Status.Equals("ApprovedByDepartmentSupervisor") || report.ReportModel.Status.Equals("ApprovedByAccountStaff");
        }

        private bool IsSubmittedOrRejected(ReportLogic report)
        {
            bool isSubmittedByConsultant = report.ReportModel.Status.Equals("SubmittedByConsultant");
            bool isRejectedByAccountStaff = report.ReportModel.Status.Equals("RejectedByAccountStaff");
            return isSubmittedByConsultant || isRejectedByAccountStaff;
        }

        public double getRemainingBudget()
        {
            remainingBudget = MONTHLY_BUDGET - TotalExpense();
            return remainingBudget;
        }

        //Report filters --------------------------------

        public List<Report> getDepartmentReports(string status)
        {
            List<Report> reports = new List<Report>();

            foreach (ReportLogic report in Reports)
            {
                if (report.ReportModel.Status == status)
                {
                    reports.Add(report.ReportModel);
                }
            }
            return reports;
        }

        public List<Report> getDepartmentReports()
        {
            List<Report> newReports = new List<Report>();
            foreach (ReportLogic report in Reports)
            {
                if (IsSubmittedOrRejected(report))
                {
                    newReports.Add(report.ReportModel);
                }
            }
            return newReports;
        }

        public bool willBeOverBudget(ReportLogic report)
        {
            return getRemainingBudget() < report.calculateExpenses();
        }
    }
}