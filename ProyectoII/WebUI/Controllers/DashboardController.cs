using AppLogic.Managers;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace WebUI.Controllers
{
    public class DashboardController : Controller
    {

        // GET: vLogin
        public ActionResult Login()
        {
            return View("login");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return View("vLogin");

        }

        public ActionResult CreatorDashboard()
        {
            return View();
        }
        public ActionResult AdminDashboard()
        {
           
            return View();
        }

        public ActionResult Paypal()
        {

            return View();
        }
        public ActionResult Profile()
        {

            return View();
        }

        public ActionResult TestNFTs()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        public ActionResult BuyerDashboard()
        {
            return View();
        }

        public ActionResult CollectionContent()
        {
            return View();
        }

        public ActionResult AuctionContent()
        {
            return View();
        }

        public ActionResult AuctionCollectionContent()
        {
            return View();
        }

        public ActionResult Offers()
        {
            return View();
        }
        public ActionResult CollectionContentCreator()
        {
            return View();
        }
    }
}