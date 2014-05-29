using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment_2.SAL;
using Assignment_2.Models;

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
            fillReports();
        }

        private void fillReports()
        {
            UsersContext userContext = new UsersContext();
            ReportContext reportContext = new ReportContext();

            List<Report> reportModels = reportContext.Reports.ToList();
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

        public double getRemainingBudget(/*string month, string year*/)
        {
            //remainingBudget = MONTHLY_BUDGET - TotalExpense(month, year);
            return remainingBudget;
        }
    }
}