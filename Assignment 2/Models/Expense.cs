using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Assignment_2.Business_Logic;
namespace Assignment_2.Models
{
    public class Expense
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public int ReportId { set; get; }

        [Required]
        [DataType(DataType.Date)]
        [CustomDateValidation(ErrorMessage = "Wrong")]
        public DateTime Date { set; get; }
        public string Description { set; get; }
        public string Location { set; get; }
        public double Amount { set; get; }
        [NotMapped]
        public string Currency { set; get; }
    }
}