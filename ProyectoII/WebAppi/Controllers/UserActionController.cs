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
    public class UserActionController : ApiController
    {
        UserActionManager um = new UserActionManager();
        [HttpPost]
        public APIResponse Post(UserAction sus)
        {
            um.CreateUserAction(sus);
            APIResponse api = new APIResponse();
            api.Message = "Registration successful";
            return api;
        }

        [HttpGet]
        public APIResponse GetAll()
        {
            return  um.RetrieveAllUserAction();
        }

        [HttpGet]
        public APIResponse Get(int id)
        {
            UserAction sus = new UserAction()
            {
                Id = id
            };
            return um.RetrieveUserAction(sus);
        }

        [HttpPost]
        public APIResponse Edit(UserAction sus)
        {

            um.UpdateUserAction(sus);
            APIResponse api = new APIResponse()
            {
                Message = "UserAction updated"
            };
            return api;
        }

        [HttpPost]
        public APIResponse Delete(int id)
        {
            UserAction sus = new UserAction()
            {
                Id = id
            };
            um.DeleteUserAction(sus);
            APIResponse api = new APIResponse()
            {
                Message = "UserAction deleted"
            };
            return api;
        }
    }
}
