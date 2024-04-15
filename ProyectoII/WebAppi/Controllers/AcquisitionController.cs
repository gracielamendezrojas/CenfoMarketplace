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
    public class AcquisitionController : ApiController
    {
        AcquisitionManager sm = new AcquisitionManager();

        [HttpPost]
        public APIResponse Post(Acquisition sus)
        {
            //Acquisition sus = new Acquisition()
            //{
            //    Id = id,
            //    CreationDate = creationDate,
            //    ClosingDate = closingDate,
            //    Price = Price,
            //    Buyer = buyer,
            //    Creator = creator
            //};
            return sm.CreateAcq(sus);

        }

        [HttpGet]
        public APIResponse GetAll()
        {
            APIResponse api = new APIResponse();
            api.Data = sm.GetAcqs();
            return api;
        }

        [HttpGet]
        public APIResponse Get(int id)
        {
            APIResponse ap = new APIResponse();
            Acquisition sus = new Acquisition()
            {
                Id = id
            };
            ap.Data = sm.GetAcq(sus);
            return ap;
        }

        [HttpPut]
        public APIResponse Edit(Acquisition sus)
        {
            sm.EditAcq(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Acquisition updated"
            };
            return api;
        }
        [HttpPut]
        public APIResponse UpdatePrice(Acquisition sus)
        {
            return sm.UpdatePrice(sus);
        }

        [HttpDelete]
        public APIResponse Delete(int id)
        {
            Acquisition sus = new Acquisition()
            {
                Id = id
            };
            sm.DeleteAcq(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Acquisition deleted"
            };
            return api;
        }
        [HttpGet]
        public APIResponse RetrieveMyAuctions(int id)
        {
            Acquisition sus = new Acquisition()
            {
                Creator = id
            };
            return sm.GetMyAuctions(sus);
        }

    }
}
