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
        List<Expense> GetAllExpenses();
        Report FindReportByPrimaryKey(int id);
        void AddReport(Report report);
        //tuan added this method for efficiency
        void AddAllExpenses(List<Expense> expenses);
        void AddExpense(Expense expense);
        void SaveChanges();
        void ChangeState(Report report, EntityState state);
        void RemoveReport(Report report);
        void RemoveExpense(Expense expense);
        void Dispose();
        bool IsMock();
    }

    public class SqlReportDataAccess : IReportDataAccess
    {
        ReportContext db = new ReportContext();

        public List<Report> GetAllReports()
        {
            return db.Reports.ToList();
        }

        public List<Expense> GetAllExpenses()
        {
            return db.Expenses.ToList();
        }
        public Report FindReportByPrimaryKey(int id)
        {
            return db.Reports.Find(id);
        }

        public void AddExpense(Expense expense)
        {
            db.Expenses.Add(expense);
        }
        public void AddReport(Report report)
        {
            db.Reports.Add(report);
        }

        public void AddAllExpenses(List<Expense> expenses)
        {
            foreach (Expense expense in expenses)
            {
                db.Expenses.Add(expense);              
            }
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

        public void RemoveExpense(Expense expense)
        {
            db.Expenses.Remove(expense);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public bool IsMock()
        {
            return false;
        }
    }

    public class MockReportDataAccess : IReportDataAccess
    {
        private List<Report> reports;
        private List<Expense> expenses;

        public MockReportDataAccess()
        {
            reports = new List<Report>();
            expenses = new List<Expense>();
        }

        public List<Report> GetAllReports()
        {
            return reports;
        }

        public List<Expense> GetAllExpenses()
        {
            return expenses;
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

        public void AddAllExpenses(List<Expense> expenses)
        {
            foreach (Expense expense in expenses)
            {
                expenses.Add(expense);
            }
        }

        public void AddExpense(Expense expense)
        {
            expenses.Add(expense);
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

        public void RemoveExpense(Expense expense)
        {
           //
        }

        public void Dispose()
        {
            //db.Dispose();
        }

        public bool IsMock()
        {
            return true;
        }
    }


}