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
    public class NftController : ApiController
    {
        NFTManager manager = new NFTManager();

        [HttpPost]
        public APIResponse Create(NFT nft)
        {
            manager.CreateNft(nft);
            APIResponse api = new APIResponse()
            {
                Message = "NFT created successfully "
            };
            return api;
        }

        [HttpGet]
        public APIResponse Retrieve(int nft)
        {
            return manager.RetrieveNft(nft);
        }
        [HttpGet]
        public APIResponse RetrieveNFTById(int id)
        {
            return manager.RetrieveOne(id);
        }
        [HttpGet]
        public APIResponse RetrieveAll()
        {
            return manager.RetrieveAllNfts();
        }

        

        [HttpGet]
        public APIResponse RetrieveByUser(int id)
        {

            return manager.RetrieveNftsByUser(id);
            
        }

        [HttpGet]
        public APIResponse RetrieveByCollection(int collection)
        {
            return manager.RetrieveByCollection(collection);            
            
        }

        
        [HttpGet]
        public APIResponse RetrieveByCollectionContent(int id)
        {
            return manager.RetrieveByCollectionContent(id);

        }

        [HttpGet]
        public APIResponse RetrieveByCollectionAuction(int collection)
        {
            return manager.RetrieveByCollectionAuction(collection);

        }


        [HttpGet]
        public APIResponse RetrieveByName(string name)
        {

            return manager.RetrieveNftByName(name);

        }

        [HttpGet]
        public APIResponse RetrieveActive()
        {
            return manager.RetrieveActive();
        }

        [HttpPost]
        public APIResponse Update(NFT nft)
        {
            return manager.UpdateNFT(nft);
        }


        [HttpPut]
        public APIResponse UpdateStatus(int id)
        {
            NFT nft = new NFT(); 
            nft.Id = id;
            return manager.UpdateStatus(nft);
        }
        [HttpPost]
        public APIResponse Delete(NFT nft)
        {
            return manager.DeleteNFT(nft);
        }
        [HttpDelete]
        public APIResponse DeleteById(int id)
        {
            return manager.DeleteNFTById(id);
        }

    }
}
