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
    public class NFTXAcquisitionController : ApiController
    {
        NFTXAcquisitionManager sm = new NFTXAcquisitionManager();
        [HttpPost]
        public APIResponse Create(NFTXAcquisition sus)
        {
            return sm.CreateNFTXAcq(sus);
        }

     
    }
}

