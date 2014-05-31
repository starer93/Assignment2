using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment_2.Models;

namespace Assignment_2.Business_Logic
{
    public class DepartmentSupervisorLogic
    {
        public string Username { get; private set; }
        public DepartmentLogic Department { get; private set; }
        public DepartmentSupervisorLogic(string username)
        {
            this.Username = username;
            loadDepartment();
        }

        private void loadDepartment()
        {
            UsersContext userContext = new UsersContext();
            var departmentName = from user in userContext.UserProfiles
                             where user.UserName == this.Username
                             select user.Department;
            Department = new DepartmentLogic(departmentName.Single());
        }
    }
}