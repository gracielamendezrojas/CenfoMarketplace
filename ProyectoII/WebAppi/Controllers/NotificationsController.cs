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
    public class NotificationsController : ApiController
    {
        NotificationsManager sm = new NotificationsManager();

        public APIResponse Post(Notification not)
        {
            sm.CreateNotifications(not);
            APIResponse api = new APIResponse();
            api.Message = "Registration successful";
            return api;
        }

        [HttpGet]
        public APIResponse GetAll()
        {
            APIResponse api = new APIResponse();
            api.Data = sm.GetNotifications();
            return api;
        }

        [HttpGet]
        public APIResponse GetAllUserId(int id)
        {
            APIResponse api = new APIResponse();
            Notification not = new Notification()
            {
                User = id
            };
            api.Data = sm.GetNotificationsUserId(not);
            return api;
        }


        [HttpGet]
        public Notification Get(int id)
        {
            Notification not = new Notification()
            {
                Id = id
            };
            return sm.GetNotifications(not);
        }

        [HttpPut]
        public APIResponse Edit(Notification not)
        {
            sm.EditNotifications(not);
            APIResponse api = new APIResponse()
            {
                Message = "Notification updated"
            };
            return api;
        }

        [HttpDelete]
        public APIResponse Delete(int id)
        {
            Notification not = new Notification()
            {
                Id = id
            };
            sm.DeleteNotifications(not);
            APIResponse api = new APIResponse()
            {
                Message = "Notification deleted"
            };
            return api;

        }
    }
}
