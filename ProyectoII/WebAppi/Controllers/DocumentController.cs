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
    public class DocumentController : ApiController
    {
        DocumentManager dm = new DocumentManager();

        [HttpPost]
        public APIResponse Post(Document sus)
        {
            dm.CreateDocument(sus);
            APIResponse api = new APIResponse();
            api.Message = "Registration successful";
            return api;
        }

        [HttpGet]
        public APIResponse GetAll()
        {
            APIResponse api = new APIResponse();
            api.Data = dm.RetrieveAllDocuments();
            return api;
        }

        [HttpGet]
        public APIResponse Get(int id)
        {
            Document sus = new Document()
            {
                Id = id
            };
            return dm.RetrieveDocument(sus);
        }

        [HttpPost]
        public APIResponse Edit(Document sus)
        {

            dm.UpdateDocument(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Updated"
            };
            return api;
        }

        [HttpPost]
        public APIResponse Delete(int id)
        {
            Document sus = new Document()
            {
                Id = id
            };
            dm.DeleteDocument(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Deleted"
            };
            return api;
        }
    }
}
