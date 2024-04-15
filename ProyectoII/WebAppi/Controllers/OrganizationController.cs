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
    public class OrganizationController : ApiController
    {
        OrganizationManager sm = new OrganizationManager();

        public APIResponse Post(Organization sus)
        {
            //Organization sus = new Organization()
            //{
            //    Id = id,
            //    Name = name
            //};
            sm.CreateOrg(sus);
            APIResponse api = new APIResponse();
            api.Message = "Registration successful";
            return api;
        }

        [HttpGet]
        public APIResponse GetAll()
        {
            APIResponse api = new APIResponse();
            api.Data = sm.GetOrgs();
            return api;
        }

        [HttpPost]
        public Organization Get(int id)
        {
            Organization sus = new Organization()
            {
                Id = id
            };
            return sm.GetOrg(sus);
        }

        [HttpPut]
        public APIResponse Edit(Organization sus)
        {
            //Organization sus = new Organization()
            //{
            //    Id = id,
            //    Name = name,
                
            //};
            sm.EditOrg(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Organization updated"
            };
            return api;
        }

        [HttpDelete]
        public APIResponse Delete(int id)
        {
            Organization sus = new Organization()
            {
                Id = id
            };
            sm.DeleteOrg(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Organization deleted"
            };
            return api;
        }
    }
}
