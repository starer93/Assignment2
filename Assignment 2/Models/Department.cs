using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Assignment_2.Models
{
    public enum DepartmentName
    {
        State_Services,
        Logistics_Services,
        Higher_Education_Services,

    };

    public class Department
    {
        private double monthlyBudget;
        public string Name { set; get; }
        public Department()
        {
            monthlyBudget = Convert.ToDouble(ConfigurationManager.AppSettings["MonthlyBudget"]);
        }

        public double GetMonthlyBudget()
        {
            return monthlyBudget;
        }
    }
}