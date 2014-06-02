using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment_2.Controllers;
using System.Web;
using System.Web.Mvc;
using Assignment_2.Models;
using Assignment_2.Business_Logic;

namespace Assignment_2.Tests.BusinessLogic
{
    [TestClass]
    public class DepartmentSupervisorLogicTest
    {
        #region Standard Data Definition
        private Report CreateStandardReport()
        {
            Report report = new Report();
            report.Id = 1;
            report.ConsultantId = "125";
            report.Date = "11/05/2014";
            report.Status = "SubmittedByConsultant";
            report.Expenses = new List<Expense>();
            return report;
        }

        private Expense CreateStandardExpense()
        {
            Expense expense = new Expense();
            expense.Amount = 250.0;
            expense.Date = DateTime.Parse("11/05/2014");
            expense.Description = "A nice plate of nachos";
            expense.Location = "The Star";
            expense.ReportId = 1;
            return expense;
        }
        #endregion

        [TestMethod]
        public void TestchangeReportStatus_GivenValidId_ChangesStatus()
        {
            Report report = CreateStandardReport();
            Expense expense = CreateStandardExpense();
            report.Expenses.Add(expense);
            MockReportDataAccess mockReportDataAccess = new MockReportDataAccess();
            mockReportDataAccess.AddReport(report);
            mockReportDataAccess.AddExpense(expense);

            DepartmentSupervisorLogic departmentSupervisorLogic = new DepartmentSupervisorLogic("125", mockReportDataAccess);
            departmentSupervisorLogic.changeReportStatus(1, "RejectedByConsultant");

            Assert.AreEqual(report.Status, "RejectedByConsultant");
        }

        [TestMethod]
        public void TestchangeReportStatus_GivenInvalidId_ChangesStatus()
        {
            Report report = CreateStandardReport();
            Expense expense = CreateStandardExpense();
            report.Expenses.Add(expense);
            MockReportDataAccess mockReportDataAccess = new MockReportDataAccess();
            mockReportDataAccess.AddReport(report);
            mockReportDataAccess.AddExpense(expense);

            DepartmentSupervisorLogic departmentSupervisorLogic = new DepartmentSupervisorLogic("125", mockReportDataAccess);
            departmentSupervisorLogic.changeReportStatus(150, "RejectedByConsultant");

            Assert.AreEqual(report.Status, "SubmittedByConsultant");
        }

    }
}
