using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment_2.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Assignment_2.SAL
{
    public class ReportContext: DbContext
    {
        public DbSet<Report> Reports { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}