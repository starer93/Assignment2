using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment_2.Controllers;
using Assignment_2.Business_Logic;
using System.Collections.Generic;
using Assignment_2.Models;
using System.Linq;
namespace Assignment_2.Tests.BusinessLogic
{
    [TestClass]
    public class AccountStaffLogicTest
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


        [TestMethod]
        public void TestgetDepartmentBudgets_GivenNoExpenses()
        {
            //standard user defined in DepartmentLogic mock logic copied here for easy comparison
            //List<UserProfile> users = new List<UserProfile>();
            //UserProfile user = new UserProfile();
            //user.Department = "Higher Education Services";
            //user.Role = "Consultant";
            //user.UserId = 1;
            //user.UserName = "125";

            MockReportDataAccess mockReportDataAccess = new MockReportDataAccess();

            AccountStaffLogic accountStaffLogic = new AccountStaffLogic(mockReportDataAccess);
            List<double> budgets = accountStaffLogic.getDepartmentBudget();

            Assert.IsNotNull(budgets);
            Assert.AreEqual(budgets.Count, 3);
            Assert.AreEqual(budgets.ElementAt(0), 10000.0);
            Assert.AreEqual(budgets.ElementAt(1), 10000.0);
            Assert.AreEqual(budgets.ElementAt(2), 10000.0);
        }

        [TestMethod]
        public void TestgetDepartmentBudgets_GivenOneExpense()
        {
            //standard user defined in DepartmentLogic mock logic copied here for easy comparison
            //List<UserProfile> users = new List<UserProfile>();
            //UserProfile user = new UserProfile();
            //user.Department = "Higher Education Services";
            //user.Role = "Consultant";
            //user.UserId = 1;
            //user.UserName = "125";

            Report report = CreateStandardReport();
            Expense expense = CreateStandardExpense();
            report.Expenses.Add(expense);
            MockReportDataAccess mockReportDataAccess = new MockReportDataAccess();

            mockReportDataAccess.AddReport(report);
            mockReportDataAccess.AddExpense(expense);

            AccountStaffLogic accountStaffLogic = new AccountStaffLogic(report, mockReportDataAccess);
            List<double> budgets = accountStaffLogic.getDepartmentBudget();

            Assert.IsNotNull(budgets);
            Assert.AreEqual(budgets.Count, 3);
            Assert.AreEqual(budgets.ElementAt(0), 10000.0);
            Assert.AreEqual(budgets.ElementAt(1), 10000.0);
            Assert.AreEqual(budgets.ElementAt(2), 9750.0);
        }
    }
}
