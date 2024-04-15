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
    public class TransactionWalletController : ApiController
    {
        TransactionWalletManager sm = new TransactionWalletManager();

        [HttpPost]
        public APIResponse Post(TransactionWallet sus)
        {
            return sm.CreateTransactionWallet(sus); ;
        }

        [HttpGet]
        public APIResponse GetAll()
        {
            return sm.RetrieveAllTransactionWallet(); ;
        }

        [HttpGet]
        public APIResponse Get(int id)
        {
            TransactionWallet sus = new TransactionWallet()
            {
                Id = id
            };
            return sm.RetrieveTransactionWallet(sus);
        }

        [HttpGet]
        public APIResponse GetReceiver(int id)
        {

            return sm.RetrieveTransactionWalletReceiver(id);
        }
        [HttpGet]
        public APIResponse GetSender(int id)
        {

            return sm.RetrieveTransactionWalletSender(id);
        }

        [HttpPut]
        public APIResponse Edit(TransactionWallet sus)
        {

            return sm.UpdateTransactionWallet(sus); ;
        }

        [HttpDelete]
        public APIResponse Delete(int id)
        {
            TransactionWallet sus = new TransactionWallet()
            {
                Id = id
            };

            return sm.DeleteTransactionWallet(sus); ;
        }
    }
}

