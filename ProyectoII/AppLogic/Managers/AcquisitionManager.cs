using DataAcess.Crud;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class AcquisitionManager
    {
        private AcquisitionCrudFactory acqCrudFactory;

        public AcquisitionManager()
        {
            acqCrudFactory = new AcquisitionCrudFactory();
        }

        public APIResponse CreateAcq(Acquisition acq)
        {
            var data = new Acquisition();
            APIResponse api = new APIResponse()
            {
                TransactionDate = DateTime.Now,
            };
            try
            {
                acqCrudFactory.Create(acq);
                var list = acqCrudFactory.RetrieveAll<Acquisition>();
                foreach(Acquisition retrieved in list)
                {
                    if(retrieved.Creator == acq.Creator && retrieved.ClosingDate == acq.ClosingDate && retrieved.CreationDate == acq.CreationDate && retrieved.Price == acq.Price)
                    {
                        data = retrieved; 
                    }; 
                }; 
                api.Status = 200;
                api.Data = data;
                api.Message = "Succesful";


                UserActionManager userActionManager = new UserActionManager();
                UserAction userAction = new UserAction()
                {
                    User = acq.Creator,
                    Date = new DateTime(),
                    Type = "Auction created"
                };
                userActionManager.CreateUserAction(userAction);
            }
            catch (Exception ex)
            {
                api.Data = ex;
                api.Status = "ERR";
                api.Message = "Error when process the request";

            };

            return api;
        }

        public List<Acquisition> GetAcqs()
        {
            return acqCrudFactory.RetrieveAll<Acquisition>();
        }

        public void DeleteAcq(Acquisition acq)
        {
            acqCrudFactory.Delete(acq);
        }

        public Acquisition GetAcq(Acquisition acq)
        {
            return acqCrudFactory.Retrieve<Acquisition>(acq);
        }

        public Acquisition GetAcqById(int idac)
        {
            Acquisition acq = new Acquisition() { Id=idac};
            return acqCrudFactory.Retrieve<Acquisition>(acq);
        }

        public void EditAcq(Acquisition acq)
        {
            acqCrudFactory.Update(acq);
        }

        public APIResponse UpdatePrice(Acquisition acq)
        {
            APIResponse api = new APIResponse()
            {
                TransactionDate = DateTime.Now,
            };
            var retrieved = acqCrudFactory.Retrieve<Acquisition>(acq);
            
            try
            {

                if (retrieved != null && retrieved.Price < acq.Price)
                {
                    retrieved.Price = acq.Price;
                    acqCrudFactory.Update(retrieved);
                    api.Status = 200;
                    api.Data = retrieved;
                    api.Message = "Succesful";
                }

            }
            catch (Exception ex)
            {
                api.Data = ex;
                api.Status = 400;
                api.Message = "Error when process the request";

            };
            return api; 
        }

        
        public APIResponse GetMyAuctions(Acquisition acq)
        {
            APIResponse api = new APIResponse()
            {
                TransactionDate = DateTime.Now,
            };
            var list = acqCrudFactory.RetrieveAll<Acquisition>();


            try
            {
                var listOfMyAuctions = new List<Acquisition>();
                foreach (Acquisition acquisition in list)
                {
                    if (acquisition.Creator.Equals(acq.Creator) && acquisition.ClosingDate  != acquisition.CreationDate)
                    {
                        listOfMyAuctions.Add(acquisition);
                    };
                };

                api.Status = 200;
                api.Data = listOfMyAuctions;
                api.Message = "Succesful";

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
