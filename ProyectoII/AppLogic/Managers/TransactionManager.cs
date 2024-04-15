using DataAcess.Crud;
using DTO_POJOS;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class TransactionManager : BaseManager
    {
        private TransactionCrudFactory trCrudFactory;

        public TransactionManager()
        {
            trCrudFactory = new TransactionCrudFactory();
        }

        public void CreateTransaction(Transaction Transaction)
        {
            trCrudFactory.Create(Transaction);
        }

        public List<Transaction> GetTransactions()
        {
            return trCrudFactory.RetrieveAll<Transaction>();
        }

        public void DeleteTransaction(Transaction Transaction)
        {
            trCrudFactory.Delete(Transaction);
        }

        public Transaction GetTransaction(Transaction Transaction)
        {
            return trCrudFactory.Retrieve<Transaction>(Transaction);
        }
        public void EditTransaction(Transaction Transaction)
        {
            trCrudFactory.Update(Transaction);
        }
        //public APIResponse GetAllAdminTrans()
        //{
        //    APIResponse response = new APIResponse() { Data = "failed" };
        //    response.Status = 505;
        //    response.Message = "failed";
        //    response.TransactionDate = DateTime.Now;

        //    try
        //    {
        //        //Get all acquisitions
        //        AcquisitionManager aManager = new AcquisitionManager();

        //        List<Transaction> listTransactions = GetTransactions();
        //        List<Acquisition> liAcquisition = aManager.GetAcqs();

        //        JObject j = new JObject();
        //        List<int> listIdTran = new List<int>();
        //        List<Acquisition> listaAcquisitions = new List<Acquisition>();



        //        foreach (Transaction t in listTransactions)
        //        {
        //            listIdTran.Add(t.Acquisition);
        //        }

        //        foreach(int l in listIdTran)
        //        {
        //        foreach (Acquisition a in liAcquisition )
        //        {
        //                if (a.Id == l)
        //                {
                            
        //                }
        //        }
        //        }

        //        response.Status = 200;
        //        response.Message = "Sucessfull.";
        //        response.Data = "";
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Data = ex;
        //    }
        //    return response;
        //}
    }
}