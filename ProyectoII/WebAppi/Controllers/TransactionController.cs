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
    public class TransactionController : ApiController
    {
        TransactionManager sm = new TransactionManager();

        public APIResponse Post(Transaction sus)
        {
            sm.CreateTransaction(sus);  
            APIResponse api = new APIResponse();
            api.Message = "Transaction successful";
            return api;
        }

        [HttpGet]
        public APIResponse GetAll()
        {
            APIResponse api = new APIResponse();
            api.Data = sm.GetTransactions();
            return api;
        }
        //[HttpGet]
        //public APIResponse GetAllAdmin()
        //{
        //    APIResponse api = new APIResponse();
        //    api.Data = sm.GetAllAdminTrans();
        //    return api;
        //}

        [HttpPost]
        public Transaction Get(int id)
        {
            Transaction sus = new Transaction()
            {
                Id = id
            };
            return sm.GetTransaction(sus);
        }

        [HttpPut]
        public APIResponse Edit(Transaction sus)
        {
            sm.EditTransaction(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Transaction updated"
            };
            return api;
        }

        [HttpDelete]
        public APIResponse Delete(int id)
        {
            Transaction sus = new Transaction()
            {
                Id = id
            };
            sm.DeleteTransaction(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Transaction deleted"
            };
            return api;
        }
    }
}
