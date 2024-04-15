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
    public class CollectionController : ApiController
    {
        CollectionManager manager = new CollectionManager();

        [HttpPost]
        public APIResponse Create(Collection col)
        {
            manager.CreateCollection(col);
            APIResponse api = new APIResponse()
            {
                Message = "NFT created successfully "
            };
            return api;
        }

        [HttpGet]
        public APIResponse Retrieve(Collection col)
        {
            return manager.RetrieveCollection(col);
        }

        [HttpGet]
        public APIResponse RetrieveOne(int id)
        {
            return manager.RetrieveCollectionById(id);
        }


        [HttpGet]
        public APIResponse RetrieveAll()
        {
            return manager.RetrieveAllCollections();
        }

        [HttpGet]
        public APIResponse RetrieveAllByUser(int Id)
        {
            return manager.RetrieveAllByUser(Id);
        }

        [HttpGet]
        public APIResponse RetrieveAllOnSale()
        {
            return manager.RetrieveAllOnSale();
        }

        [HttpGet]
        public APIResponse RetrieveByName(string name)
        {
            return manager.RetrieveByName(name);
        }
        [HttpGet]
        public APIResponse buyerDefaultCollection(int id)
        {
            return manager.defaultCollectionBuyer(id);
        }
        //Creo que se van a coupar no estan implementados ni testeados
        //
        //[HttpGet]
        //public APIResponse RetrieveByUser(NFT nft)
        //{
        //    manager.CreateNft(nft);
        //    APIResponse api = new APIResponse()
        //    {
        //        Message = "NFT created successfully "
        //    };
        //    return api;
        //}

        //[HttpGet]
        //public APIResponse RetrieveByCollection(NFT nft)
        //{
        //    manager.CreateNft(nft);
        //    APIResponse api = new APIResponse()
        //    {
        //        Message = "NFT created successfully "
        //    };
        //    return api;
        //}

        [HttpPost]
        public APIResponse Update(Collection col)
        {
            return manager.UpdateCollection(col);
        }

        [HttpPut]
        public APIResponse UpdateStatus(int id)
        {
            Collection col = new Collection()
            {
                Id = id
            }; 
            return manager.UpdateCollectionStatus(col);
        }

        [HttpPost]
        public APIResponse Delete(Collection col)
        {
            return manager.DeleteCollection(col);
        }

        [HttpDelete]
        public APIResponse DeleteById(int id)
        {
            return manager.DeleteCollectionById(id);
        }
    }
}
