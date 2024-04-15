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
    public class PasswordController : ApiController
    {
        PasswordManager sm = new PasswordManager();

        [HttpPost]
        public APIResponse Post(Password sus)
        {
            sm.CreatePass(sus);
            APIResponse api = new APIResponse();
            api.TransactionDate = DateTime.Now;
            api.Status = 200;
            api.Message = "Registration successful";
            return api;
        }

        [HttpGet]
        public APIResponse GetAll()
        {
            APIResponse api = new APIResponse();
            api.Data = sm.GetPasswords();
            return api;
        }

        [HttpPost]
        public Password Get(int id)
        {
            Password sus = new Password()
            {
                Id = id
            };
            return sm.GetPass(sus);
        }

        [HttpPut]
        public APIResponse Edit(Password sus)
        {
            return sm.EditPass(sus);
        }

        [HttpDelete]
        public APIResponse Delete(int id)
        {
            Password sus = new Password()
            {
                Id = id
            };
            sm.DeletePass(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Password deleted"
            };
            return api;
        }

        [HttpPost]
        public APIResponse CheckPassword(Password sus)
        {
            return sm.CheckingPassword(sus); 
        }

    }
}

