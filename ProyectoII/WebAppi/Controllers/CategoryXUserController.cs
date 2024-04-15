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
    public class CategoryXUserController : ApiController
    {
        CategoryXUserManager cm = new CategoryXUserManager();

        [HttpPost]
        public APIResponse Post(CategoryXUser sus)
        {
            cm.CreateCategoryXUser(sus);
            APIResponse api = new APIResponse();
            api.Message = "Registration successful";
            return api;
        }

        [HttpGet]
        public APIResponse GetAll()
        {
            APIResponse api = new APIResponse();
            api.Data = cm.RetrieveAllCategoryXUser();
            return api;
        }

        [HttpGet]
        public APIResponse Get(int id)
        {
            CategoryXUser sus = new CategoryXUser()
            {
                Id = id
            };
            return cm.RetrieveCategoryXUser(sus);
        }

        [HttpPost]
        public APIResponse Edit(CategoryXUser sus)
        {

            cm.UpdateCategoryXuser(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Updated"
            };
            return api;
        }

        [HttpPost]
        public APIResponse Delete(int id)
        {
            CategoryXUser sus = new CategoryXUser()
            {
                Id = id
            };
            cm.DeleteCategoryXUser(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Deleted"
            };
            return api;
        }
    }
}
