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
    public class FeeManager : BaseManager
    {

        private FeeCrudFactory feeCrudFactory;

        public FeeManager()
        {
            feeCrudFactory = new FeeCrudFactory();
        }

   
        public APIResponse retrieveCurrentFee()
        {
            APIResponse api = new APIResponse()
            {
                //Data = 
                Message = "Not found",
                TransactionDate = DateTime.Now,
                Status = 400,
            };


            var retrieved = feeCrudFactory.RetrieveAll<Fee>();
            try
            {
                if (retrieved != null)
                {
                    api.Status = 200;
                    api.Data = retrieved;
                    api.Message = "Succesful";
                }
            }
            catch (Exception ex)
            {
                api.Data = ex;
                api.Status = "ERR";
                api.Message = "Error when process the request";

            };

            return api;
        }


        public decimal retrieveFee()
        {

            var amount = 0; 
            var retrieved = feeCrudFactory.RetrieveAll<Fee>();
            foreach (var fee in retrieved)
            {
                amount = (int)fee.Amount;
            }; 
            return amount;
        }

        public APIResponse stablishNewFee(FeeUserAction feeUserAction)
        {
            Fee fee = new Fee()
            {
                Amount = feeUserAction.Amount,
            };
            APIResponse api = new APIResponse()
            {
                TransactionDate = DateTime.Now, 
            };
            try
            {
                feeCrudFactory.Update(fee);
                api.Data = fee;
                api.Status = 200;
                api.Message = "Succesful";


                UserActionManager um = new UserActionManager();
                UserAction ua = new UserAction()
                {
                    User = feeUserAction.Admin,
                    Date = new DateTime(),
                    Type = "Fee updated"
                };

                um.CreateUserAction(ua);

            }
            catch (Exception ex)
            {
                api.Data = ex;
                api.Status = "ERR";
                api.Message = "Error when process the request";

            };



            return api;
        }
    }
}
