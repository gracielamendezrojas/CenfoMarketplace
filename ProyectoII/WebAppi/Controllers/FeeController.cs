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
    public class FeeController : ApiController
    {
        FeeManager fm = new FeeManager();

        [HttpGet]
        public APIResponse GetFee()
        {
            return fm.retrieveCurrentFee();
        }

        [HttpGet]
        public decimal GetFeeNumber()
        {
            return fm.retrieveFee();
        }


        [HttpPut]
        public APIResponse Edit(FeeUserAction feeUserAction)
        {
            return fm.stablishNewFee(feeUserAction);
        }
    }
}
