using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace Assignment_2.Controllers
{
    public class ConsultantController : Controller
    {
        //
        // GET: /Consultant/
        [Authorize(Roles="Consultant")]
        public ActionResult Index()
        {
            return View();
        }

    }
}
