using DataAcess.Crud;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class NFTXAcquisitionManager
    {
        private NFTXAcquisitionCrudFactory NFTXAcqCrudFactory;

        public NFTXAcquisitionManager()
        {
            NFTXAcqCrudFactory = new NFTXAcquisitionCrudFactory();
        }

        public APIResponse CreateNFTXAcq(NFTXAcquisition NFTXAcq)
        {
            APIResponse response = new APIResponse()
            {
                TransactionDate = DateTime.Now,
            };

            try
            {
                NFTXAcqCrudFactory.Create(NFTXAcq);
                response.Status = "200";
                response.Message = "Succesfull";
                response.Data = NFTXAcq;
            }
            catch (Exception)
            {
                response.Status = "400"; 
                response.Message = "Error";
                response.Data = null;
            }; 
            return response; 
        }

        public List<NFTXAcquisition> GetNFTXAcq()
        {
            return NFTXAcqCrudFactory.RetrieveAll<NFTXAcquisition>();
        }

        public void DeleteNFTXAcq(NFTXAcquisition NFTXAcq)
        {
            NFTXAcqCrudFactory.Delete(NFTXAcq);
        }

        public NFTXAcquisition GetNFTXACq(NFTXAcquisition NFTXAcq)
        {
            return NFTXAcqCrudFactory.Retrieve<NFTXAcquisition> (NFTXAcq);
        }
        public void EditBid(Bid bid)
        {
            NFTXAcqCrudFactory.Update(bid);
        }
    }
}
