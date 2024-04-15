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
    public class RoleController : ApiController
    {
        RoleManager sm = new RoleManager();

        [HttpPost]
        public APIResponse Create(Role sus)
        {
            sm.CreateRole(sus);
            APIResponse api = new APIResponse();
            api.Message = "Registration successful";
            return api;
        }

        [HttpGet]
        public APIResponse GetAll()
        {
            APIResponse api = new APIResponse();
            api.Data = sm.RetrieveAllRole();
            return api;
        }

        [HttpGet]
        public APIResponse Get(Role sus)
        {
            APIResponse api = new APIResponse();
            api.Data = sm.RetrieveRole(sus);
            return api;
        }

        [HttpPut]
        public APIResponse Edit(Role sus)
        {
            sm.UpdateRole(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Role updated"
            };
            return api;
        }

        [HttpDelete]
        public APIResponse Delete(Role sus)
        {
            sm.DeleteRole(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Role deleted"
            };
            return api;
        }
    }
}