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
    public class AutoBidManager:BaseManager
    {
        private AutoBidCrudFactory autobidCrudFactory;

        public AutoBidManager()
        {
            autobidCrudFactory = new AutoBidCrudFactory();
        }

        public APIResponse Create(AutoBid AutoBid)
        {
            var data = new Acquisition();
            APIResponse api = new APIResponse()
            {
                TransactionDate = DateTime.Now,
            };

            List<AutoBid> listAutoBids = GetAutoBids();

            if (listAutoBids.Contains(AutoBid))
            {
                autobidCrudFactory.Update(AutoBid);
            }
            else
            {
                autobidCrudFactory.Create(AutoBid);
            }                  
            return api;
        }

        public List<AutoBid> GetAutoBids()
        {
            return autobidCrudFactory.RetrieveAll<AutoBid>();
        }

        public void DeleteAcq(AutoBid acq)
        {
            autobidCrudFactory.Delete(acq);
        }

        public AutoBid GetMyAutobid(AutoBid auto)
        {   
            AutoBid a = new AutoBid();
            List<AutoBid> listAutoBids = GetAutoBids();

            foreach (AutoBid item in listAutoBids)
            {
                if (item.Equals(auto))
                {
                    a = item;
                }
            }

            return a;
        }


        public AutoBid DelMyAutobid(AutoBid auto)
        {
            AutoBid a = new AutoBid();
            List<AutoBid> listAutoBids = GetAutoBids();

            foreach (AutoBid item in listAutoBids)
            {
                if (item.Equals(auto))
                {
                    auto.AutoBidId = item.AutoBidId;
                    autobidCrudFactory.Delete(auto);
                }
            }
            return a;
        }

        public void EditAcq(AutoBid acq)
        {
            autobidCrudFactory.Update(acq);
        }

    }
}
