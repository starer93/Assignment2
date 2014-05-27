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
    public class ExpenseController : Controller
    {
        private ReportContext db = new ReportContext();

        //
        // GET: /Expense/

        public ActionResult Index()
        {
            return View(db.Expenses.ToList());
        }

        //
        // GET: /Expense/Details/5

        public ActionResult Details(int id = 0)
        {
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        //
        // GET: /Expense/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Expense/Create

        [HttpPost]
        public ActionResult Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Expenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expense);
        }

        //
        // GET: /Expense/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        //
        // POST: /Expense/Edit/5

        [HttpPost]
        public ActionResult Edit(Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        //
        // GET: /Expense/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        //
        // POST: /Expense/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Expense expense = db.Expenses.Find(id);
            db.Expenses.Remove(expense);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}