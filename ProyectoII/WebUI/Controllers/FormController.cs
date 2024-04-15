using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class FormController : Controller
    {
        public ActionResult UserForm()
        {
            return View();
        }

        public ActionResult OTPEmail()
        {
            return View();
        }
        public ActionResult OTPSMS()
        {
            return View();
        }

        public ActionResult Document()
        {
            return View();
        }
    }
}