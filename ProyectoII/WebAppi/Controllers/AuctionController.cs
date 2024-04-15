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
    public class AuctionController : ApiController
    {
        AuctionManager sm = new AuctionManager();

        //[HttpPost]
        //public APIResponse Post(Acquisition sus)
        //{
        //    //Acquisition sus = new Acquisition()
        //    //{
        //    //    Id = id,
        //    //    CreationDate = creationDate,
        //    //    ClosingDate = closingDate,
        //    //    Price = Price,
        //    //    Buyer = buyer,
        //    //    Creator = creator
        //    //};
        //    return sm.CreateAcq(sus);

        //}

        //[HttpGet]
        //public APIResponse GetAll()
        //{
        //    APIResponse api = new APIResponse();
        //    api.Data = sm.GetAcqs();
        //    return api;
        //}

        //[HttpPost]
        //public Acquisition Get(int id)
        //{
        //    Acquisition sus = new Acquisition()
        //    {
        //        Id = id
        //    };
        //    return sm.GetAcq(sus);
        //}

        //[HttpPut]
        //public APIResponse Edit(Acquisition sus)
        //{
        //    //Acquisition sus = new Acquisition()
        //    //{
        //    //    Id = id,
        //    //    CreationDate = creationDate,
        //    //    ClosingDate = closingDate,
        //    //    Price = Price,
        //    //    Buyer = buyer,
        //    //    Creator = creator

        //    //};
        //    sm.EditAcq(sus);
        //    APIResponse api = new APIResponse()
        //    {
        //        Message = "Acquisition updated"
        //    };
        //    return api;
        //}

        //[HttpDelete]
        //public APIResponse Delete(int id)
        //{
        //    Acquisition sus = new Acquisition()
        //    {
        //        Id = id
        //    };
        //    sm.DeleteAcq(sus);
        //    APIResponse api = new APIResponse()
        //    {
        //        Message = "Acquisition deleted"
        //    };
        //    return api;
        //}
        [HttpGet]
        public APIResponse RetrieveAllNFTAuctions()
        {
            return sm.GetAllNFTAuctions();
        }

        [HttpGet]
        public APIResponse RetrieveAllCollectionAuctions()
        {
            return sm.GetAllCollectionAuctions();
        }

        [HttpGet]
        public APIResponse RetrieveMyAuctionsNFT(int id)
        {
            Auction sus = new Auction()
            {
                Creator = id
            };
            return sm.GetMyAuctionsNFT(sus);
        }


        [HttpGet]
        public APIResponse RetrieveMyAuctionsCollection(int id)
        {
            Auction sus = new Auction()
            {
                Creator = id
            };
            return sm.GetMyAuctionsCollection(sus);
        }

        [HttpPost]
        public APIResponse DateComparison(StartDate DateAuction)
        {
            return sm.DateComparison(DateAuction);
        }

        [HttpGet]
        public APIResponse RetrieveMyAuctionsBuyer(int id)
        {
            return sm.GetMyAuctionsBuyer(id);
        }
    }
}
