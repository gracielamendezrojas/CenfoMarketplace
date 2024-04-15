using AppLogic.Managers;
using DTO_POJOS;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAppi.Controllers
{
    public class CategoryController : ApiController
    {
        CategoryManager manager = new CategoryManager();

        [HttpPost]
        public APIResponse Create(JObject category)
        {
            manager.CreateCategory(category);
            APIResponse api = new APIResponse()
            {
                Message = "Category created successfully "
            };
            return api;
        }

        [HttpGet]
        public APIResponse Retrieve(Category category)
        {
            return manager.RetrieveCategory(category);
        }

        [HttpGet]
        public APIResponse RetrieveAll()
        {
            return manager.RetrieveAllCategories();
        }

        [HttpPost]
        public APIResponse Update(Category category)
        {
            return manager.UpdateCategory(category);
        }

        [HttpPost]
        public APIResponse Delete(Category category)
        {
            return manager.DeleteCategory(category);
        }

    }
}
