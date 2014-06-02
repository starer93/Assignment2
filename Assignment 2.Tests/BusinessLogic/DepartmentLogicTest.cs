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
    public class DepartmentLogicTest
    {
        #region Standard Data Definition
        private Report CreateStandardReport()
        {
            Report report = new Report();
            report.Id = 1;
            report.ConsultantId = "125";
            report.Date = "11/05/2014";
            report.Status = "ApprovedByAccountStaff"; //Different to standard because reports need to be approved to add to the department's budget
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
        //standard user defined in DepartmentLogic mock logic copied here for easy comparison
        //List<UserProfile> users = new List<UserProfile>();
        //UserProfile user = new UserProfile();
        //user.Department = "Higher Education Services";
        //user.Role = "Consultant";
        //user.UserId = 1;
        //user.UserName = "125";

        [TestMethod]
        public void TestgetRemainingBudget_GivenOneExpense()
        {
            Report report = CreateStandardReport();
            Expense expense = CreateStandardExpense();
            report.Expenses.Add(expense);
            MockReportDataAccess mockReportDataAccess = new MockReportDataAccess();
            mockReportDataAccess.AddReport(report);
            mockReportDataAccess.AddExpense(expense);

            DepartmentLogic departmentLogic = new DepartmentLogic("Higher Education Services", mockReportDataAccess);

            Assert.AreEqual(departmentLogic.getRemainingBudget(), 9750.0);

        }

        [TestMethod]
        public void TestgetRemainingBudget_GivenMultipleExpenses()
        {
            Report report = CreateStandardReport();
            Expense expense = CreateStandardExpense();
            report.Expenses.Add(expense);
            report.Expenses.Add(expense);
            report.Expenses.Add(expense);
            MockReportDataAccess mockReportDataAccess = new MockReportDataAccess();
            mockReportDataAccess.AddReport(report);
            mockReportDataAccess.AddExpense(expense);
            mockReportDataAccess.AddExpense(expense);
            mockReportDataAccess.AddExpense(expense);
            DepartmentLogic departmentLogic = new DepartmentLogic("Higher Education Services", mockReportDataAccess);

            Assert.AreEqual(departmentLogic.getRemainingBudget(), 9250.0);
        }

        [TestMethod]
        public void TestgetDepartmentReportsWithStatus_GivenValidReportAndStatus()
        {
            Report report = CreateStandardReport();
            Expense expense = CreateStandardExpense();
            report.Expenses.Add(expense);
            MockReportDataAccess mockReportDataAccess = new MockReportDataAccess();
            mockReportDataAccess.AddReport(report);
            mockReportDataAccess.AddExpense(expense);

            DepartmentLogic departmentLogic = new DepartmentLogic("Higher Education Services", mockReportDataAccess);
            List<Report> returnedReports = departmentLogic.getDepartmentReports("ApprovedByAccountStaff");

            Assert.IsNotNull(returnedReports);
            Assert.AreEqual(returnedReports.FirstOrDefault(), report);

        }

        [TestMethod]
        public void TestgetDepartmentReportsWithStatus_GivenValidReportAndInvalidStatus()
        {
            Report report = CreateStandardReport();
            Expense expense = CreateStandardExpense();
            report.Expenses.Add(expense);
            MockReportDataAccess mockReportDataAccess = new MockReportDataAccess();
            mockReportDataAccess.AddReport(report);
            mockReportDataAccess.AddExpense(expense);

            DepartmentLogic departmentLogic = new DepartmentLogic("Higher Education Services", mockReportDataAccess);
            List<Report> returnedReports = departmentLogic.getDepartmentReports("SubmittedByConsultant");

            Assert.IsTrue(returnedReports.Count==0);
        }

        [TestMethod]
        public void TestgetDepartmentReportsWithStatus_GivenNoReportAndValidStatus()
        {

            MockReportDataAccess mockReportDataAccess = new MockReportDataAccess();

            DepartmentLogic departmentLogic = new DepartmentLogic("Higher Education Services", mockReportDataAccess);
            List<Report> returnedReports = departmentLogic.getDepartmentReports("ApprovedByAccountStaff");

            Assert.IsTrue(returnedReports.Count==0);
        }

        [TestMethod]
        public void TestgetDepartmentReports_GivenValidReport()
        {
            Report report = CreateStandardReport();
            Expense expense = CreateStandardExpense();
            report.Expenses.Add(expense);
            MockReportDataAccess mockReportDataAccess = new MockReportDataAccess();
            mockReportDataAccess.AddReport(report);
            mockReportDataAccess.AddExpense(expense);
            report.Status = "SubmittedByConsultant";
            DepartmentLogic departmentLogic = new DepartmentLogic("Higher Education Services", mockReportDataAccess);
            List<Report> returnedReports = departmentLogic.getDepartmentReports();

            Assert.IsTrue(returnedReports.Count!=0);

        }

    }
}
