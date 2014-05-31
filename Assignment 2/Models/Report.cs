using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment_2.Models
{
    public class Report
    {
        public int Id{set; get;}
        public string ConsultantId { set; get; }
        public string ApprovedBy { set; get; }
        public string Status { set; get; }
        public string Date { set; get; }
        public byte[] Receipt { get; set; }
        public virtual ICollection<Expense> Expenses { set; get; }
    }
}