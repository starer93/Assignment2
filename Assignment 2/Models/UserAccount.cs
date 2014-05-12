using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment_2.Models
{
    public abstract class UserAccount
    {
        public string UserId { protected set; get; }
        public List<Report> Reports { protected set; get; }
        public Department department { set; get; }

        protected UserAccount(string userId)
        {
            UserId = userId;
        }
    }

    public class Consultant: UserAccount
    {
        public Consultant(string userId) : base (userId)
        {

        }
    }

    public class Supervisor: UserAccount
    {
        public Supervisor(string userId) : base (userId)
        {

        }
    }

    public class AccountStaff: UserAccount
    {
        public AccountStaff(string userId) : base (userId)
        {
            department = null;
        }
    }
}