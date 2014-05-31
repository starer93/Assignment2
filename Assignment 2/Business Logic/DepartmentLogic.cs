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

        public DepartmentLogic(string departmentName)
        {
            Reports = new List<ReportLogic>();
            Name = departmentName;
            loadReports();
        }

        private void loadReports()
        {
            UsersContext userContext = new UsersContext();
            IReportDataAccess reportDataAccess = new SqlReportDataAccess();

            List<Report> reportModels = reportDataAccess.GetAllReports();
            List<UserProfile> users = userContext.UserProfiles.ToList();
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

        public double getTotalBudget()
        {
            return MONTHLY_BUDGET;
        }

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

        public List<ReportLogic> getDepartmentReports(string month, string year, string status)
        {
            List<ReportLogic> newReports = new List<ReportLogic>();
            foreach (ReportLogic report in Reports)
            {
                if (report.ReportModel.Status == status)
                {
                    newReports.Add(report);
                }
            }
            return newReports;
        }

        public List<ReportLogic> getDepartmentReports(string month, string year)
        {
            List<ReportLogic> newReports = new List<ReportLogic>();
            foreach (ReportLogic report in Reports)
            {
                if (IsSubmittedOrRejected(report))
                {
                    newReports.Add(report);
                }
            }
            return newReports;
        }

        public bool willBeOverBudget(ReportLogic report)
        {
            return remainingBudget < report.calculateExpenses();
        }
    }
}