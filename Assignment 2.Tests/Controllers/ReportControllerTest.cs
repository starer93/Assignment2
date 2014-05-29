using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment_2.Controllers;
using System.Collections.Generic;
using Assignment_2.Models;
namespace Assignment_2.Tests.Controllers
{
    [TestClass]
    public class ReportControllerTest
    {
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

            mockReportDataAccess.AddReport(new Report
            {
                ConsultantId = "125",
                Date = "11/05/2014",
                Status = "SubmittedByConsultant"
            });

            mockReportDataAccess.AddReport(new Report
            {
                ConsultantId = "125",
                Date = "11/05/2014",
                Status = "SubmittedByConsultant"
            });


            ReportController reportController = new ReportController(mockReportDataAccess);
            List<Report> reports = reportController.reportDataAccess.GetAllReports();
            Assert.IsNotNull(reports);
            Assert.AreEqual(reports.Count, 2);
        }
    }
}
