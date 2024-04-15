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
    public class WalletController : ApiController
    {
        WalletManager sm = new WalletManager();

        [HttpPost]
        public APIResponse Create(Wallet sus)
        {
            sm.CreateWallet(sus);
            APIResponse api = new APIResponse();
            api.Message = "Registration successful";
            return api;
        }

        [HttpGet]
        public APIResponse GetAll()
        {
            APIResponse api = new APIResponse();
            api.Data = sm.RetrieveAllWallet();
            return api;
        }

        [HttpGet]
        public APIResponse Get(string Id)
        {
            APIResponse api = new APIResponse();
            Wallet w = new Wallet()
            {
                Id = Id
            };
            api.Status = 200;
            api.Data = sm.RetrieveWallet(w);
            return api;
        } 

        [HttpPut]
        public APIResponse Edit(Wallet sus)
        {
            sm.UpdateWallet(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Wallet updated",
                Status = 200
            };
            return api;
        }
        

        [HttpPut]
        public APIResponse EditByUser(Wallet sus)
        {

            sm.UpdateByUser(sus);

            APIResponse api = new APIResponse()
            {
                Message = "Wallet updated",
                Status = 200
            };
            return api;
        }
        [HttpGet]
        public APIResponse GetByUser(int Id)
        {
            APIResponse api = new APIResponse();
            Wallet w = new Wallet()
            {
                UserId = Id
            };
            api.Data = sm.RetrieveByUser(w);
            api.Status = 200;
            return api;
        }

        [HttpDelete]
        public APIResponse Delete(Wallet sus)
        {
            sm.DeleteWallet(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Wallet deleted"
            };
            return api;
        }
    }
}