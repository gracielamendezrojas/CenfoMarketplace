using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_POJOS
{
    public class AutoBid : BaseEntity
    {
        public int AutoBidId { get; set; }
        public int AquisitionId { get; set; }
        public decimal Amount { get; set; }
        public decimal MaxAmount { get; set; }
        public decimal Increment { get; set; }
        public int UserId { get; set; }


        public override bool Equals(object subs)
        {
            if (subs == null)
            {
                return false;
            }
            if (!(subs is AutoBid))
            {
                return false;
            }
            return (this.AquisitionId == ((AutoBid)subs).AquisitionId & this.UserId == ((AutoBid)subs).UserId);

        }
    }
}
