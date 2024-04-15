using DataAcess.Crud;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class PayPalManager
    {
        private PayPalCrudFactory payCrudFactory;

        public PayPalManager()
        {
            payCrudFactory = new PayPalCrudFactory();
        }

        public APIResponse CreatePay(Paypal pay)
        {
            APIResponse api = new APIResponse();
            api.TransactionDate = DateTime.Now;
             payCrudFactory.Create(pay);

            try
            {
                api.Message = "Registration successful";
                api.Data = pay;
                api.Status = 200;
            } catch (Exception)
            {
                api.Status = 400;
                api.Message = "Not successful";
            }

            return api;
        }

        public APIResponse GetPays()
        {
            APIResponse api = new APIResponse();
            api.TransactionDate = DateTime.Now;

            try
            {
                var list = payCrudFactory.RetrieveAll<Paypal>();
                api.Message = "List";
                api.Data = list;
                api.Status = 200;
            }
            catch (Exception)
            {
                api.Status = 400;
                api.Message = "Not successful";
            }

            return api;
        }

        public APIResponse DeletePay(Paypal pay)
        {
            APIResponse api = new APIResponse();
            api.TransactionDate = DateTime.Now;

            var retrieved = payCrudFactory.Retrieve<Paypal>(pay);
            try
            {
                if (retrieved != null)
                {
                    payCrudFactory.Delete(pay);
                    api.Message = "Deleted";
                    api.Data = pay;
                    api.Status = 200;
                }

            }
            catch (Exception)
            {
                api.Status = 400;
                api.Message = "Not successful";
            }
            return api;
        }



        public APIResponse GetPay(Paypal pay)
        {

            APIResponse api = new APIResponse();
            api.TransactionDate = DateTime.Now;

            try
            {    
                var retrieved = payCrudFactory.Retrieve<Paypal>(pay);
                api.Message = "Retrieved";
                api.Data = retrieved;
                api.Status = 200;

            }
            catch (Exception)
            {
                api.Status = 400;
                api.Message = "Not successful";
            }
            return api;
        }
        public APIResponse EditPay(Paypal pay)
        {

            APIResponse api = new APIResponse();

            api.Message = "Updated";
            api.Data = payCrudFactory.RetrieveAll<Paypal>(); ;
            api.TransactionDate = DateTime.Now;
            payCrudFactory.Update(pay);

            return api;
        }
    }
}
