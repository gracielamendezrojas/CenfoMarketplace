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
    public class PayPalController : ApiController
    {
        PayPalManager sm = new PayPalManager();

        [HttpPost]
        public APIResponse Create(Paypal sus)
        { 
            return sm.CreatePay(sus);
        }

        [HttpGet]
        public APIResponse GetAll()
        {
            APIResponse api = new APIResponse();
            api.Data = sm.GetPays();
            return api;
        }

        [HttpGet]
        public APIResponse Get(int Id)
        {
            Paypal sus = new Paypal()
            {
                UserId = Id
            };
            return sm.GetPay(sus);
        }

        [HttpPut]
        public APIResponse Update(Paypal sus)
        {
            return sm.EditPay(sus);
        }

        [HttpDelete]
        public APIResponse Delete(String Id)
        {
            Paypal sus = new Paypal()
            {
                OrderId = Id            
            };

            return sm.DeletePay(sus);
        }
    }
}
