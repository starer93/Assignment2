using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment_2.Controllers;
using Assignment_2.Business_Logic;
using System.Collections.Generic;
using Assignment_2.Models;
namespace Assignment_2.Tests.BusinessLogic
{
    [TestClass]
    public class ReportLogicTest
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
        public void TestReportLogicConstructor_ReturnsCorrectData_GivenStandardData()
        {
            Report report = CreateStandardReport();
            Expense expense = CreateStandardExpense();
            report.Expenses.Add(expense);

            ReportLogic reportLogic = new ReportLogic(report);

            Assert.IsNotNull(reportLogic);
            Assert.AreEqual(report.Id, reportLogic.ReportModel.Id);
            Assert.AreEqual(report.ConsultantId, reportLogic.ReportModel.ConsultantId);        
            Assert.AreEqual(report.Date, reportLogic.ReportModel.Date);
            Assert.AreEqual(report.Status, reportLogic.ReportModel.Status);
            Assert.AreEqual(report.Expenses, reportLogic.ReportModel.Expenses);
            
        }

        [TestMethod]
        public void TestCalculateExpenses_GivenOneExpense()
        {
            Report report = CreateStandardReport();
            Expense expense = CreateStandardExpense();         
            report.Expenses.Add(expense);
            ReportLogic reportLogic = new ReportLogic(report);

            double returnedExpenses = reportLogic.calculateExpenses();

            Assert.IsNotNull(returnedExpenses);
            Assert.AreEqual(returnedExpenses, 250.0);
        }


        [TestMethod]
        public void TestCalculateExpenses_GivenMultipleExpenses()
        {
            Report report = CreateStandardReport();
            Expense expense = CreateStandardExpense();
            report.Expenses.Add(expense);
            report.Expenses.Add(expense);
            report.Expenses.Add(expense);
            ReportLogic reportLogic = new ReportLogic(report);

            double returnedExpenses = reportLogic.calculateExpenses();

            Assert.IsNotNull(returnedExpenses);
            Assert.AreEqual(returnedExpenses, 750.0);
        }

        [TestMethod]
        public void TestCalculateExpenses_GivenNoExpenses()
        {
            Report report = CreateStandardReport();
            ReportLogic reportLogic = new ReportLogic(report);

            double returnedExpenses = reportLogic.calculateExpenses();

            Assert.IsNotNull(returnedExpenses);
            Assert.AreEqual(returnedExpenses, 0);
        }
    }
}
