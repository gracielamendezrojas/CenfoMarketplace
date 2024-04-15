using AppLogic.Managers;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAppi.Controllers
{
    public class AutoBidController : ApiController
    {
        AutoBidManager sm = new AutoBidManager();


        [HttpGet]
        public APIResponse GetAllAutoBids()
        {
            APIResponse api = new APIResponse();
            api.Data = sm.GetAutoBids();
            api.Status = "200";
            api.Message = "Success";
            api.TransactionDate = DateTime.Now;
            return api;
        }

        [HttpPost]
        public APIResponse PostAutobid(AutoBid a)
        {
            return sm.Create(a);
        }


        [HttpPost]
        public APIResponse GetMyAutobid(AutoBid a)
        {
            APIResponse api = new APIResponse();
            api.Data = sm.GetMyAutobid(a);
            api.Status = "200";
            api.Message = "Success";
            api.TransactionDate = DateTime.Now;
            return api;
        }

        [HttpPost]
        public APIResponse DelMyAutobid(AutoBid a)
        {
            APIResponse api = new APIResponse();
            api.Data = sm.DelMyAutobid(a);
            api.Status = "200";
            api.Message = "Deleted";
            api.TransactionDate = DateTime.Now;
            return api;
        }

    }
}