using AppLogic.Managers;
using DataAcess.Crud;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class TransactionWalletManager : BaseManager
    {

        private TransactionWalletCrudFactory TransactionWalletCrudFactory;

        public TransactionWalletManager()
        {
            TransactionWalletCrudFactory = new TransactionWalletCrudFactory();
        }

        public APIResponse CreateTransactionWallet(TransactionWallet TransactionWallet)
        {
            APIResponse response = new APIResponse() { Message = "TransactionWallets not found." };
            response.TransactionDate = DateTime.Now;
            
            try
            {
                var date = DateTime.Now;
                TransactionWallet.TransactionDate = date;
                TransactionWalletCrudFactory.Create(TransactionWallet);
               
                response.Status = 200;
                response.Data = TransactionWallet;
                response.Message = "Sucessfull";
            }
            catch(Exception ex )
            {
                response.Data = ex;
                response.Status = 404;
                response.Message = "Error when process the request";
            }
            return response;
            
        }

        public APIResponse RetrieveAllTransactionWallet()
        {
            APIResponse response = new APIResponse() { Message = "TransactionWallets not found." };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            List<TransactionWallet> TransactionWallet = TransactionWalletCrudFactory.RetrieveAll<TransactionWallet>();
            try
            {
                if (TransactionWallet != null)
                {
                    response.Status = 200;
                    response.Data = TransactionWallet;
                    response.Message = "Sucessfull";
                }
            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Status = "ERR";
                response.Message = "Error when process the request";

            }

            return response;
        }

        public APIResponse RetrieveTransactionWallet(TransactionWallet TransactionWallet)
        {
            APIResponse response = new APIResponse() { Message = "TransactionWallet not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            var retrieved = TransactionWalletCrudFactory.Retrieve<TransactionWallet>(TransactionWallet);

            try
            {

                if (retrieved != null)
                {
                    response.Status = 200;
                    response.Data = retrieved;
                    response.Message = "Sucessfull";
                }

            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Status = "ERR";
                response.Message = "Error when process the request";

            }


            return response;
        }
        public APIResponse RetrieveTransactionWalletReceiver(int id)
        {
            WalletManager wManager = new WalletManager();
            APIResponse response = new APIResponse() { Message = "Error" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;
            var walletNumber = "";


            try
            {
               

                var listaWallets = wManager.RetrieveAllWallet();
                foreach (Wallet p in listaWallets)
                {
                    if (p.UserId == id)
                    {
                        walletNumber = p.Id;
                    }
                }

                var list = TransactionWalletCrudFactory.RetrieveAll<TransactionWallet>();
                List<TransactionWallet> listaTransactions = new List<TransactionWallet>();
                foreach(TransactionWallet t in list)
                {
                    if (t.WalletReceiver == walletNumber)
                    {
                        listaTransactions.Add(t);
                    }
                }

                    response.Status = 200;
                    response.Data = listaTransactions;
                response.Message = "Sucessfull";

            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Status = "ERR";
                response.Message = "Error when process the request";

            }


            return response;
        }
        public APIResponse RetrieveTransactionWalletSender(int id)
        {
            WalletManager wManager = new WalletManager();
            APIResponse response = new APIResponse() { Message = "Error" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;
            var walletNumber = "";


            try
            {


                var listaWallets = wManager.RetrieveAllWallet();
                foreach (Wallet p in listaWallets)
                {
                    if (p.UserId == id)
                    {
                        walletNumber = p.Id;
                    }
                }

                var list = TransactionWalletCrudFactory.RetrieveAll<TransactionWallet>();
                List<TransactionWallet> listaTransactions = new List<TransactionWallet>();
                foreach (TransactionWallet t in list)
                {
                    if (t.WalletSender == walletNumber)
                    {
                        listaTransactions.Add(t);
                    }
                }

                response.Status = 200;
                response.Data = listaTransactions;
                response.Message = "Sucessfull";

            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Status = "ERR";
                response.Message = "Error when process the request";

            }


            return response;
        }

        public APIResponse UpdateTransactionWallet(TransactionWallet TransactionWallet)
        {
            APIResponse response = new APIResponse() { Message = "TransactionWallet not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            var retrieved = TransactionWalletCrudFactory.Retrieve<TransactionWallet>(TransactionWallet);

            TransactionWallet.TransactionDate = DateTime.Now;
            try
            {

                if (retrieved != null)
                {
                    TransactionWalletCrudFactory.Update(TransactionWallet);
                    response.Status = 200;
                    response.Data = TransactionWallet;
                    response.Message = "Update Sucessfull";
                }


            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Status = "ERR";
                response.Message = "Error when process the request";

            }

            return response;


        }

        public APIResponse DeleteTransactionWallet(TransactionWallet TransactionWallet)
        {
            APIResponse response = new APIResponse() { Message = "TransactionWallet not found" };
            response.Status = 404;

            var retrieved = TransactionWalletCrudFactory.Retrieve<TransactionWallet>(TransactionWallet);


            try
            {
                if (retrieved != null)
                {
                    TransactionWalletCrudFactory.Delete(TransactionWallet);

                    response.Status = 200;
                    response.Data = retrieved;
                    response.Message = "Delete Sucessfull";
                }

            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Status = "ERR";
                response.Message = "Error when process the request";

            }
            return response;

        }
    }
}
