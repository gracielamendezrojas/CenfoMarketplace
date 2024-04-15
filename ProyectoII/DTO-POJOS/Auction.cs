using System;

namespace DTO_POJOS
{
    public class Auction : BaseEntity
    {
        public int AcquisitionId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public decimal AuctionPrice { get; set; }
        public int Buyer { get; set; }
        public int Creator { get; set; }
        public int NFT { get; set; }
        public string NFTName { get; set; }
        public string NFTStatus { get; set; }
        public decimal NFTPrice { get; set; }
        public int Collection { get; set; }
        public string CollectionName { get; set; }
        public string CollectionSaleStatus { get; set; }


        public string FormattedCreationDate
        {
            get { return CreationDate.ToString("MM/dd/yyyy HH:mm"); }
        }
        public string FormattedClosingDate
        {
            get { return ClosingDate.ToString("MM/dd/yyyy HH:mm"); }
        }

        public override bool Equals(object subs)
        {
            if (subs == null)
            {
                return false;
            }
            if (!(subs is Auction))
            {
                return false;
            }
            return (this.AcquisitionId == ((Auction)subs).AcquisitionId);
        }

    }
}
