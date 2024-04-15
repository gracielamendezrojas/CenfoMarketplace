using DataAcess.Crud;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class AuctionManager
    {
        private AuctionCrudFactory auctionCrudFactory;

        public AuctionManager()
        {
            auctionCrudFactory = new AuctionCrudFactory();
        }



        public APIResponse GetAllNFTAuctions()
        {
            APIResponse api = new APIResponse()
            {
                TransactionDate = DateTime.Now,
            };
            var now = DateTime.Now;
            try
            {
                var list = auctionCrudFactory.RetrieveAll<Auction>();
                var listOfNFTAuctions = new List<Auction>();
                HashSet<int> uniqueNFT = new HashSet<int>();

                if (list != null)
                {

                    foreach (Auction auction in list)
                    {
                        if (auction.ClosingDate != auction.CreationDate && auction.CollectionSaleStatus != "Auction" && auction.NFTStatus.Equals("Auction") && auction.ClosingDate > now)
                        {
                            bool isNewAuction = uniqueNFT.Add(auction.NFT);
                            if (isNewAuction)
                            {
                                listOfNFTAuctions.Add(auction);
                            }
                        };
                    };

                    api.Status = 200;
                    api.Data = listOfNFTAuctions; 
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




        public APIResponse GetAllCollectionAuctions()
        {
            APIResponse api = new APIResponse()
            {
                TransactionDate = DateTime.Now,
            };
            var list = auctionCrudFactory.RetrieveAll<Auction>();
            HashSet<int> uniqueCollection = new HashSet<int>();

            try
            {
                var listOfMyAuctions = new List<Auction>();
                foreach (Auction auction in list)
                {
                    if (auction.ClosingDate != auction.CreationDate && auction.CollectionSaleStatus.Equals("Auction"))
                    {
                        bool isNewAuction = uniqueCollection.Add(auction.Collection);
                        if (isNewAuction)
                        {
                            listOfMyAuctions.Add(auction);
                        }
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
        public APIResponse DateComparison(StartDate date)
        {
            var now = DateTime.Now;

            APIResponse api = new APIResponse()
            {
                TransactionDate = now,
                Data = date.DateAuction,
            };
            if(date.DateAuction <= now)
            {
                api.Status = 400;
                api.Message = "Error when process the request";
            }
            else
            {
                api.Status = 200;
                api.Message = "Succesful";
            }
            return api; 
        }

        public APIResponse GetMyAuctionsNFT(Auction acq)
        {
            APIResponse api = new APIResponse()
            {
                TransactionDate = DateTime.Now,
            };
            var list = auctionCrudFactory.RetrieveAll<Auction>();
            var now = DateTime.Now;


            try
            {
                var listOfMyAuctions = new List<Auction>();
                HashSet<int> uniqueNFT = new HashSet<int>();

                foreach (Auction auction in list)
                {
                    if (auction.Creator.Equals(acq.Creator) && auction.ClosingDate  != auction.CreationDate && auction.CollectionSaleStatus != "Auction" && auction.NFTStatus.Equals("Auction"))
                    {
                        bool isNewAuction = uniqueNFT.Add(auction.NFT);
                        if (isNewAuction)
                        {
                            listOfMyAuctions.Add(auction);

                        }

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


        public APIResponse GetMyAuctionsCollection(Auction acq)
        {
            APIResponse api = new APIResponse()
            {
                TransactionDate = DateTime.Now,
            };
            var list = auctionCrudFactory.RetrieveAll<Auction>();
            HashSet<int> uniqueCollection = new HashSet<int>();

            try
            {
                var listOfMyAuctions = new List<Auction>();
                foreach (Auction auction in list)
                {
                    if (auction.Creator.Equals(acq.Creator) && auction.ClosingDate != auction.CreationDate && auction.CollectionSaleStatus.Equals("Auction"))
                    {
                        bool isNewAuction = uniqueCollection.Add(auction.Collection);
                        if (isNewAuction)
                        {
                            listOfMyAuctions.Add(auction);
                        }
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

        public APIResponse GetMyAuctionsBuyer(int id)
        {
            APIResponse api = new APIResponse()
            {
                TransactionDate = DateTime.Now,
            };

            List<Auction> list = auctionCrudFactory.RetrieveAll<Auction>();

            BidManager bm = new BidManager();

            try
            {
                List<Auction> listOfMyAuctions = new List<Auction>();
               
                foreach (Auction auction in list)
                {
                    foreach (Bid bid in bm.GetBids())
                    {
                        if(bid.User == id && bid.Acquisition == auction.AcquisitionId)
                        {
                            if (!listOfMyAuctions.Contains(auction) && DateTime.Compare(DateTime.Now, auction.ClosingDate) < 0)
                            {
                                listOfMyAuctions.Add(auction);
                            }
                        }
                    }
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
