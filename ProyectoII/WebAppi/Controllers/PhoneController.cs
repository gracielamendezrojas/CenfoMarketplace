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
    public class PhoneController : ApiController
    {
        PhoneManager sm = new PhoneManager();

        public APIResponse Post(Phone sus)
        {

            sm.CreatePhone(sus);
            APIResponse api = new APIResponse();
            api.Message = "Registration successful";
            return api;
        }

        [HttpGet]
        public APIResponse GetAll()
        {
            APIResponse api = new APIResponse();
            api.Data = sm.GetPhones();
            return api;
        }

        [HttpPost]
        public Phone Get(int id)
        {
            Phone sus = new Phone()
            {
                Id = id
            };
            return sm.GetPhone(sus);
        }

        public APIResponse GetByUser(int id)
        {
            return sm.GetPhoneByUser(id);
        }

        [HttpPut]
        public APIResponse Edit(Phone sus)
        {

            sm.EditPhone(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Phone updated"
            };
            return api;
        }

        [HttpDelete]
        public APIResponse Delete(int id)
        {
            Phone sus = new Phone()
            {
                Id = id
            };
            sm.DeletePhone(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Phone deleted"
            };
            return api;
        }
    }
}
