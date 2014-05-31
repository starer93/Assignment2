using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
namespace Assignment_2.Models
{
    public class Expense
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public int ReportId { set; get;}
        public string Date { set; get; }
        public string Description { set; get; }
        public string Location { set; get; }
        public double Amount { set; get; }
    }
}