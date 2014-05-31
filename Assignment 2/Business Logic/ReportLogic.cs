using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment_2.Models;

namespace Assignment_2.Business_Logic
{
    public class ReportLogic
    {
        public Report ReportModel { get; private set; }

        public ReportLogic(Report reportModel)
        {
            ReportModel = reportModel;
        }

        public double calculateExpenses()
        {
            double sum = 0;
            foreach (Expense expense in ReportModel.Expenses)
            {
                sum += expense.Amount;
            }
            return sum;
        }
    }
}