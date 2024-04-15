using DataAccess.Crud;
using DataAcess.Crud;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class BidManager
    {
        private BidCrudFactory bidCrudFactory;
        private AutoBidCrudFactory autoBidCrudFactory;

        public BidManager()
        {
            bidCrudFactory = new BidCrudFactory();
            autoBidCrudFactory = new AutoBidCrudFactory();

        }

        public void CreateBid(Bid bid)
        {
            AcquisitionManager acm = new AcquisitionManager();
            Acquisition ac = acm.GetAcqById(bid.Acquisition);
            UserActionManager um = new UserActionManager();
             UserAction ua = new UserAction()
             {
                 User = bid.User,
                 Date = new DateTime(),
                 Type = "Bid " + bid.Id + " created by user " + bid.User
             };
             um.CreateUserAction(ua);

            // check if BID is hiher than current best price

            bidCrudFactory.Create(bid);
            ac.Price = bid.Amount;
            acm.UpdatePrice(ac);
            // check if there are auto bid
            // if true - call back same function with each auto buyer

            gestionarAutoBid(bid);
        }
        public APIResponse GetHighestBid(int acquisition)
        {
            APIResponse apiResponse = new APIResponse()
            {
                TransactionDate = new DateTime(),
                Status = 400
        };
            try
            {
                List<Bid> list = bidCrudFactory.RetrieveAll<Bid>();
                if(list != null)
                {
                    List<Bid> auctions = new List<Bid>();
                    foreach (Bid bid in list)
                    {
                        if (bid.Acquisition.Equals(acquisition))
                        {
                            auctions.Add(bid);
                        }
                    }

                    

                    if(auctions != null)
                    {

                        if (auctions.Count==0) { 
                            Bid h = new Bid();
                            h.Acquisition = acquisition;
                            h.Amount = 0;
                            auctions.Add((Bid)h);
                            apiResponse.Status = 400;
                        }
                        else
                        {
                            apiResponse.Status = 200;
                        }



                        var highestAmount = auctions[0].Amount;
                        Bid highestBid = auctions[0];
                        foreach (Bid bid in auctions)
                        {
                            if (bid.Amount > highestAmount)
                            {
                                highestAmount = bid.Amount;
                                highestBid = bid;
                            }
                        }
                        apiResponse.Data = highestBid;
                        apiResponse.Message = "Succesfull";
                    }
                    
                }


            }catch (Exception ex)
            {
                apiResponse.Message = "error";
                apiResponse.Status = 400;
                apiResponse.Data = ex; 
            }
            return apiResponse;
        }
        public List<Bid> GetBids()
        {
            return bidCrudFactory.RetrieveAll<Bid>();
        }

        public void DeleteBid(Bid bid)
        {
            UserActionManager um = new UserActionManager();
            UserAction ua = new UserAction()
            {
                User = bid.User,
                Date = new DateTime(),
                Type = "User " + bid.User + " created"
            };
            um.CreateUserAction(ua);

            bidCrudFactory.Delete(bid);
        }

        public Bid GetBid(Bid bid)
        {
            return bidCrudFactory.Retrieve<Bid>(bid);
        }
        public void EditBid(Bid bid)
        {
            bidCrudFactory.Update(bid);
        }

        public List<Bid> GetBidsByAq(int aq)
        {
            List<Bid> allaq = bidCrudFactory.RetrieveAll<Bid>();
            List<Bid> allaqfiltered = new List<Bid>();


            foreach (Bid bid in allaq)
            {
                if(bid.Acquisition== aq)
                {
                    allaqfiltered.Add(bid);
                }
            }

            return allaqfiltered;
        }


        public void gestionarAutoBid(Bid bid)
        {
            AcquisitionManager acm = new AcquisitionManager();
            Acquisition ac = acm.GetAcqById(bid.Acquisition);

            List<AutoBid> autobidlist = autoBidCrudFactory.RetrieveAll<AutoBid>();

            foreach (AutoBid item in autobidlist)
            {
                if (item.AquisitionId == bid.Acquisition && (bid.Amount + item.Increment) < item.MaxAmount && bid.User != item.UserId)
                {
                    Bid bid1 = new Bid()
                    {
                        Id = 0,
                        Amount = bid.Amount + item.Increment,
                        Date = DateTime.Now,
                        Acquisition = item.AquisitionId,
                        User = item.UserId,
                        Type = bid.Type
                    };
                    ac.Price = bid1.Amount;
                    CreateBid(bid1);
                    acm.UpdatePrice(ac);
                    Notify(bid1);
                }
            }
        }

        public Bid GetMyLastBid(int arquisition, int user)
        {
            List<Bid> allaq = bidCrudFactory.RetrieveAll<Bid>();
            Bid tempbid = new Bid() { Acquisition = arquisition, User = user, Amount = 0 };


            foreach (Bid bid in allaq)
            {
                if (bid.Acquisition == tempbid.Acquisition && bid.User == tempbid.User && tempbid.Amount < bid.Amount)
                {
                    tempbid = bid;
                }

            }

            return tempbid;
        }



        public void Notify(Bid bid)
        {
            //Notifications
            BidManager sm = new BidManager();
            List<Bid> currentbids = sm.GetBidsByAq(bid.Acquisition);
            NotificationsManager nm = new NotificationsManager();
            NotificationManager em = new NotificationManager();
            UserManager um = new UserManager();
            PhoneManager phoneManager = new PhoneManager();
            Phone phone = new Phone();
            HashSet<int> uniqueUser = new HashSet<int>();
            List<Bid> bidsNotification = new List<Bid>();



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
        }
    }
}
