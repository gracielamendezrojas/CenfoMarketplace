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
    public class RoleXUserController : ApiController
    {
        RoleXUserManager sm = new RoleXUserManager();

        [HttpPost]
        public APIResponse Create(RoleXUser sus)
        {
            sm.CreateRoleXUser(sus);
            APIResponse api = new APIResponse();
            api.Message = "Registration successful";
            return api;
        }

        [HttpGet]
        public APIResponse GetAll()
        {
            APIResponse api = new APIResponse();
            api.Data = sm.RetrieveAllRoleXUser();
            return api;
        }

        [HttpGet]
        public APIResponse Get(RoleXUser sus)
        {
            APIResponse api = new APIResponse();
            api.Data = sm.RetrieveRoleXUser(sus);
            return api;
        }

        [HttpPut]
        public APIResponse Edit(RoleXUser sus)
        {
            sm.UpdateRoleXUser(sus);
            APIResponse api = new APIResponse()
            {
                Message = "RoleXUser updated"
            };
            return api;
        }

        [HttpDelete]
        public APIResponse Delete(RoleXUser sus)
        {
            sm.DeleteRoleXUser(sus);
            APIResponse api = new APIResponse()
            {
                Message = "RoleXUser deleted"
            };
            return api;
        }
    }
}