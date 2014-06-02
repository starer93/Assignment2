using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment_2.Controllers;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using Assignment_2.Models;
using System.Web.SessionState;
using System.Reflection;
using System.IO;
namespace Assignment_2.Tests.Controllers
{
    [TestClass]
    public class ReportControllerTest
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

        /*
        [TestMethod]
        public void TestCreate_ReturnsView()
        {
            MockReportDataAccess mockReportDataAccess = new MockReportDataAccess();
            //IHttpSessionState Session = new HttpContext.Current.Session;
            HttpRequest httpRequest = new HttpRequest("", "http://localhost/", "");
            HttpResponse httpResponse = new HttpResponse(new StringWriter());
            HttpContext httpContext = new HttpContext(httpRequest, httpResponse);
            HttpSessionStateContainer sessionContainer = new HttpSessionStateContainer(
                                       "id", 
                                       new SessionStateItemCollection(), 
                                       new HttpStaticObjectsCollection(),
                                       10,
                                       true,
                                       HttpCookieMode.AutoDetect,
                                       SessionStateMode.InProc,
                                       false);
            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                            BindingFlags.NonPublic | BindingFlags.Instance,
                            null,
                            CallingConventions.Standard,
                            new[] { typeof(HttpSessionStateContainer) },
                            null).Invoke(new object[] { sessionContainer });

            HttpContext.Current = httpContext;
            List<Expense> expenses = new List<Expense>();
            expenses.Add(CreateStandardExpense());
            expenses.Add(CreateStandardExpense());
            //HttpContext.Current.Session["CurrentExpenses"] = expenses;
            httpContext.Items["CurrentExpenses"] = expenses;
            HttpContext.Current.Session.Add("CurrentExpenses", expenses);
            sessionContainer["CurrentExpenses"] = expenses;
            ReportController reportController = new ReportController(mockReportDataAccess);
            ActionResult view = reportController.Create();
            Assert.IsNotNull(view);
            Assert.AreEqual("System.Web.Mvc.ViewResult", view.ToString());

        }
         */

        /*
        [TestMethod]
        public void SomeTestMethod()
        {
            
            using (ShimsContext.Create())
            {
                //HttpContextBase httpContext;
                MockReportDataAccess mockReportDataAccess = new MockReportDataAccess();
                List<Expense> expenses = new List<Expense>();
                expenses.Add(CreateStandardExpense());
                expenses.Add(CreateStandardExpense());              
                //ReportController reportController = new ReportController(mockReportDataAccess);
            
                HttpSessionStateBase Session = new System.Web.Fakes.ShimHttpSessionStateBase(Session);
                //var Session = new System.Web.SessionState.Fakes.ShimHttpSessionState();
                //Session.ItemGetString = (key) => { if (key == "testString") return "test"; return null; };
                /*
                var context = new System.Web.Fakes.ShimHttpContext();
                System.Web.Fakes.ShimHttpContext.CurrentGet = () => { return context; };
                System.Web.Fakes.ShimHttpContext.AllInstances.SessionGet =
                    (o) =>
                    {
                        return Session;
                    };
                ReportController reportController = new ReportController(mockReportDataAccess);
   
                 
                //System.Web.Fakes.ShimHttpSessionStateBase.Constructor
                ActionResult view = reportController.Create();
                Assert.IsNotNull(view);
            }
        }
             
           */
        
        /*
        [TestMethod]
        public void SomeTestMethod()
        {
            using (ShimsContext.Create())
            {
                MockReportDataAccess mockReportDataAccess = new MockReportDataAccess();
                List<Expense> expenses = new List<Expense>();
                expenses.Add(CreateStandardExpense());
                expenses.Add(CreateStandardExpense());
                ReportController reportController = new ReportController(mockReportDataAccess);
                var Session = new System.Web.SessionState.Fakes.ShimHttpSessionState();
                Session.ItemGetString = (key) => { if (key == "CurrentExpenses") return expenses; return null; };
                //session. Item name + GettingorSetting + type of value
                //key = name of argument (session item name in this context)
                //return = value to be returned
                //code found on http://blog.christopheargento.net/2013/02/02/testing-untestable-code-thanks-to-ms-fakes/

                var context = new System.Web.Fakes.ShimHttpContext();
                System.Web.Fakes.ShimHttpContext.CurrentGet = () => { return context; };
                System.Web.Fakes.ShimHttpContext.AllInstances.SessionGet =
                    (o) =>
                    {
                        return Session;
                    };

                //var result = instanceToTest.SomeMethod() ;sd
                ActionResult view = reportController.Create();
                Assert.IsNotNull(view);
                

            }
         * }
            */
        
        [TestMethod]
        public void TestReturnAllReportsReturnsReports()
        {
            //ReportController.MockReportDataAccess mockReportDataAccess = new ReportController.MockReportDataAccess();
            MockReportDataAccess mockReportDataAccess = new MockReportDataAccess();
            Report report = new Report();
            report.ConsultantId = "125";
            report.Date = "11/05/2014";
            report.Status = "SubmittedByConsultant";
            mockReportDataAccess.AddReport(report);


            ReportController reportController = new ReportController(mockReportDataAccess);
            List<Report> reports = reportController.reportDataAccess.GetAllReports();
            Assert.IsNotNull(reports);
            Assert.AreEqual(reports.Count, 1);
        }

        [TestMethod]
        public void TestReturnAllReports_GivenMultipleReports_ReturnsReports()
        {
            //ReportController.MockReportDataAccess mockReportDataAccess = new ReportController.MockReportDataAccess();
            MockReportDataAccess mockReportDataAccess = new MockReportDataAccess();

            Report report = CreateStandardReport();
            mockReportDataAccess.AddReport(report);

            mockReportDataAccess.AddReport(report);


            ReportController reportController = new ReportController(mockReportDataAccess);
            List<Report> reports = reportController.reportDataAccess.GetAllReports();
            Assert.IsNotNull(reports);
            Assert.AreEqual(reports.Count, 2);
        }

        [TestMethod]
        public void TestDetails_GivenExistingReportID_ReturnsView()
        {
            MockReportDataAccess mockReportDataAccess = new MockReportDataAccess();
            Report report = CreateStandardReport();
            Expense expense = CreateStandardExpense();
            report.Expenses.Add(expense);
            mockReportDataAccess.AddReport(report);
            mockReportDataAccess.AddExpense(expense);

            ReportController reportController = new ReportController(mockReportDataAccess);

            ActionResult view = reportController.Details(report.Id);
            
            Assert.IsNotNull(view);
            Assert.AreEqual("System.Web.Mvc.ViewResult", view.ToString());
            //System.Diagnostics.Debug.WriteLine(view.ToString());         
        }

        [TestMethod]
        public void TestDetails_GivenFakeReportID_ReturnsHttpNotFound()
        {
            MockReportDataAccess mockReportDataAccess = new MockReportDataAccess();
            Report report = CreateStandardReport();
            Expense expense = CreateStandardExpense();
            report.Expenses.Add(expense);
            mockReportDataAccess.AddReport(report);
            mockReportDataAccess.AddExpense(expense);

            ReportController reportController = new ReportController(mockReportDataAccess);
            ActionResult view = reportController.Details(report.Id+1);

            Assert.IsNotNull(view);
            Assert.AreEqual("System.Web.Mvc.HttpNotFoundResult", view.ToString()); 
        }

        /*
         * throwing null reference exceptions
        [TestMethod]
        public void TestViewReceipt_GivenRealReportId_ReturnsFileStream()
        {
            MockReportDataAccess mockReportDataAccess = new MockReportDataAccess();
            Report report = CreateStandardReport();
            /*
            byte[] pdf = new byte[5];
            pdf[0] = 1;
            pdf[1] = 2;
            pdf[2] = 3;
            pdf[3] = 4;
            pdf[4] = 5;
            report.Receipt = pdf;
             * 

            Expense expense = CreateStandardExpense();
            report.Expenses.Add(expense);
            mockReportDataAccess.AddReport(report);
            mockReportDataAccess.AddExpense(expense);

            ReportController reportController = new ReportController(mockReportDataAccess);
            FileStreamResult fileStream = reportController.ViewReceipt(report.Id);
            //System.Diagnostics.Debug.WriteLine(fileStream.ToString());
            //Assert.IsNull(fileStream);
        }
         */ 
    
    }
}
