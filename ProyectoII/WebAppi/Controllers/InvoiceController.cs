using AppLogic.Managers;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAppi.Controllers
{
    public class InvoiceController : ApiController
    {
        //InvoiceManager sm = new InvoiceManager();

        [HttpPost]
        public APIResponse Create(Invoice sus)
        {
            //sm.CreateInvoice(sus);
            APIResponse api = new APIResponse();
            api.Message = "Registration successful";
            return api;
        }

        [HttpGet]
        public APIResponse GetAll()
        {
            APIResponse api = new APIResponse();
           //api.Data = sm.RetrieveAllInvoice();
            return api;
        }

        [HttpGet]
        public APIResponse Get(Invoice sus)
        {
            APIResponse api = new APIResponse();
            //api.Data = sm.RetrieveInvoice(sus);
            return api;
        }

        [HttpPut]
        public APIResponse Edit(Invoice sus)
        {
            //sm.UpdateInvoice(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Invoice updated"
            };
            return api;
        }

        [HttpDelete]
        public APIResponse Delete(Invoice sus)
        {
            //sm.DeleteInvoice(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Invoice deleted"
            };
            return api;
        }
        [HttpPost]
        public async Task<APIResponse> SentEmailInvoice(Invoice invoice)
        {
            APIResponse api = new APIResponse() { Status = 500, TransactionDate = DateTime.Now };

            NotificationManager sm = new NotificationManager();
          
            var resultu = await sm.emailInvoice(invoice);

            if (resultu)
            {
                api.Data = invoice.Email;
                api.Status = 200;
                api.Message = "Email Sent successfuly";
                api.TransactionDate = DateTime.Now;
            }

            return api;
        }
    }
}