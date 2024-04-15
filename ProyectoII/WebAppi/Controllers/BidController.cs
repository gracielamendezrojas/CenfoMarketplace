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
    public class BidController : ApiController
    {
        BidManager sm = new BidManager();

        [HttpPost]
        public APIResponse Post(Bid bid)
        {
            bid.Date = DateTime.Now;    
            bool ishighestbit = false;
            APIResponse api = new APIResponse();
            List<Bid> currentbids = sm.GetBidsByAq(bid.Acquisition); // no 
            Auction currentAuction= new Auction();
            AuctionManager am = new AuctionManager();
            AcquisitionManager acm = new AcquisitionManager();
            Acquisition ac = acm.GetAcqById(bid.Acquisition);
            Bid highestbit = (Bid)GetHighestBid(bid.Acquisition).Data;
            List<Bid> bidsNotification = new List<Bid>();
            HashSet<int> uniqueUser = new HashSet<int>();

            if (bid.Type == 1)//NFT
            {
                List<Auction> la = (List<Auction>)am.GetAllNFTAuctions().Data;
                foreach (Auction a in la)
                {
                    if (a.AcquisitionId == bid.Acquisition)
                    {
                        currentAuction = a;
                    }
                }
            }

            if (bid.Type==2)//Collection
            {
                List<Auction> la = (List<Auction>)am.GetAllCollectionAuctions().Data;
                foreach (Auction a in la)
                {
                    if (a.AcquisitionId == bid.Acquisition)
                    {
                        currentAuction = a;
                    }
                }
            }

            api.Status = 500;
            api.Message = "Bid Rejected";


            // Check if current bid is highest than all bids in Auction
            if(ac.Price < bid.Amount)
            {
                ishighestbit = true;
            }


            if (ishighestbit) {
                sm.CreateBid(bid);
                ac.Price = bid.Amount;
                acm.UpdatePrice(ac);
                api.Data = bid;
                api.Status = 200;
                api.Message = "Bid accepted";  
            }



            //Notifications
            NotificationsManager nm = new NotificationsManager();
            NotificationManager em = new NotificationManager();
            UserManager um = new UserManager();
            PhoneManager phoneManager = new PhoneManager();
            Phone phone = new Phone();


            foreach (Bid bd in currentbids)
            {
                if (bd.User != bid.User)
                {
                    bool isNewUser = uniqueUser.Add(bd.User);
                    if (isNewUser)
                    {
                        bidsNotification.Add(bd);
                    }
                }
            }


            if (bidsNotification != null)
            {
                foreach (Bid b in bidsNotification)
                {
                    Notification notification = new Notification()
                    {
                        User = b.User,
                        Message = "A new bid has been made.",
                        Subject = "A higher bid has been made."
                    };
                    nm.CreateNotifications(notification);
                    User user = new User()
                    {
                        Id = b.User
                    };
                    var userInformation = um.GetUser(user);

                    if (userInformation.PreferredMethod == "Email")
                    {
                        em.bidNotification(userInformation.Email);
                    }
                    else if (userInformation.PreferredMethod == "Both")
                    {
                        em.bidNotification(userInformation.Email);

                        foreach (Phone ph in phoneManager.GetPhones())
                        {
                            if (ph.User == b.User)
                            {
                                phone = ph;
                                break;
                            }
                        }
                        em.phonebidNotification(phone.Number);
                    }
                    else
                    {
                        foreach (Phone ph in phoneManager.GetPhones())
                        {
                            if (ph.User == b.User)
                            {
                                phone = ph;
                                break;
                            }
                        }
                        em.phonebidNotification(phone.Number);
                    }
                }
            }


            return api;
        }

        [HttpGet]
        public APIResponse GetAll()
        {
            APIResponse api = new APIResponse();
            api.Data = sm.GetBids();
            return api;
        }

        [HttpPost]
        public Bid Get(int id)
        {
            Bid sus = new Bid()
            {
                Id = id
            };
            return sm.GetBid(sus);
        }

        [HttpPut]
        public APIResponse Edit(Bid sus)
        {
            sm.EditBid(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Bid updated"
            };
            return api;
        }

        [HttpGet]
        public APIResponse GetHighestBid(int id)
        {
            return sm.GetHighestBid(id);
        }
        [HttpDelete]
        public APIResponse Delete(int id)
        {
            Bid sus = new Bid()
            {
                Id = id
            };
            sm.DeleteBid(sus);
            APIResponse api = new APIResponse()
            {
                Message = "Bid deleted"
            };
            return api;
        }

        [HttpGet]
        public APIResponse RetrieveAllByAquistion(int id)
        {
            APIResponse api = new APIResponse();
            api.Data = sm.GetBidsByAq(id);
            return api;
        }

        [HttpGet]
        public APIResponse RetrieveMyLastBid(int arquisition, int user)
        {
            APIResponse api = new APIResponse();
            api.Data = sm.GetMyLastBid(arquisition, user);
            return api;
        }

    }
}

