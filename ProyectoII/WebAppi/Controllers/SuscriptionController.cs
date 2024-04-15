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
    public class SuscriptionController : ApiController
    {
        SuscriptionManager sm = new SuscriptionManager();

        [HttpPost]
        public APIResponse Post(Suscription sus)
        {
            sm.CreateSuscription(sus);
            APIResponse api = new APIResponse();
            api.Message = "Registration successful";
            return api;
        }

        [HttpGet]
        public APIResponse GetAll()
        {
            return sm.RetrieveAllSuscription();
        }

        [HttpGet]
        public APIResponse Get(int id)
        {
            Suscription sus = new Suscription()
            {
                Id = id
            };
            return sm.RetrieveSuscription(sus);
        }

        [HttpPost]
        public APIResponse Edit(Suscription sus)
        {

            sm.UpdateSuscription(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Updated"
            };
            return api;
        }

        [HttpPost]
        public APIResponse Delete(int id)
        {
            Suscription sus = new Suscription()
            {
                Id = id
            };
            sm.DeleteSuscription(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Deleted"
            };
            return api;
        }
    }
}
