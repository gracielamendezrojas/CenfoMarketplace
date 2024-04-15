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
    public class SuscriptionManager : BaseManager
    {

        private SuscriptionCrudFactory suscriptionCrudFactory;

        public SuscriptionManager()
        {
            suscriptionCrudFactory = new SuscriptionCrudFactory();
        }

        public void CreateSuscription(Suscription suscription)
        {
            var date = DateTime.Now;
            suscription.SuscriptionDate = date;
            suscription.Id = suscriptionCrudFactory.RetrieveAll<Suscription>().Count + 1;

            suscriptionCrudFactory.Create(suscription);
        }

        public APIResponse RetrieveAllSuscription()
        {
            APIResponse response = new APIResponse() { Message = " Not found." };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            List<Suscription> suscription = suscriptionCrudFactory.RetrieveAll<Suscription>();
            try
            {
                    if (suscription != null){
                        response.Status = 200;
                        response.Data = suscription;
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

        public APIResponse RetrieveSuscription(Suscription suscription)
        {
            APIResponse response = new APIResponse() { Message = " Not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            var retrieved = suscriptionCrudFactory.Retrieve<Suscription>(suscription);

            try
            {

                if (retrieved != null) {
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

        public APIResponse UpdateSuscription(Suscription suscription)
        {
            APIResponse response = new APIResponse() { Message = "Not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            var retrieved = suscriptionCrudFactory.Retrieve<Suscription>(suscription);

            suscription.SuscriptionDate = DateTime.Now;
            try
            {

                if (retrieved != null)
                {
                    suscriptionCrudFactory.Update(suscription);
                    response.Status = 200;
                    response.Data = suscription;
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

        public APIResponse DeleteSuscription(Suscription suscription)
        {
            APIResponse response = new APIResponse() { Message = "Not found" };
            response.Status = 404;

            var retrieved = suscriptionCrudFactory.Retrieve<Suscription>(suscription);


            try
            {
                if (retrieved != null){
                    suscriptionCrudFactory.Delete(suscription);

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
