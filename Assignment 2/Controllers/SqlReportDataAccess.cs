using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment_2.Models;
using Assignment_2.SAL;

namespace Assignment_2.Controllers
{
    public interface IReportDataAccess
    {
        List<Report> GetAllReports();
        Report FindReportByPrimaryKey(int id);
        void AddReport(Report report);
        void SaveChanges();
        void ChangeState(Report report, EntityState state);
        void RemoveReport(Report report);
        void Dispose();
    }

    public class SqlReportDataAccess : IReportDataAccess
    {
        ReportContext db = new ReportContext();

        public List<Report> GetAllReports()
        {
            return db.Reports.ToList();
        }
        public Report FindReportByPrimaryKey(int id)
        {
            return db.Reports.Find(id);
        }
        public void AddReport(Report report)
        {
            db.Reports.Add(report);
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }
        public void ChangeState(Report report, EntityState state)
        {
            db.Entry(report).State = state;
        }
        public void RemoveReport(Report report)
        {
            db.Reports.Remove(report);
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }

    public class MockReportDataAccess : IReportDataAccess
    {
        private List<Report> reports;

        public MockReportDataAccess()
        {
            reports = new List<Report>();
        }

        public List<Report> GetAllReports()
        {
            return reports;
        }

        public Report FindReportByPrimaryKey(int id)
        {
            return reports.FirstOrDefault(x => x.Id == id);

            //Report report = new Report();
            //report.Id = 15;
            //if (id == 15)
            //{
            //    return report;
            //}
            //else
            //    return null;
        }

        public void AddReport(Report report)
        {
            reports.Add(report);
        }

        public void SaveChanges()
        {
            //db.SaveChanges();
        }

        public void ChangeState(Report report, EntityState state)
        {
            //db.Entry(report).State = state;
        }

        public void RemoveReport(Report report)
        {
            //db.Reports.Remove(report);
        }

        public void Dispose()
        {
            //db.Dispose();
        }
    }


}