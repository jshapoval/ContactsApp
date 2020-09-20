using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactsApp.DAL;

namespace ContactsApp.Controllers
{
    public class HomeController : Controller
    {
        private ContactContext db = new ContactContext();
        public ActionResult Index()
        {
              return View();
        }

    }
}