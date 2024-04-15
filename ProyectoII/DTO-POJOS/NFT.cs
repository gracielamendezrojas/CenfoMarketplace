using System;

namespace DTO_POJOS
{
    public class NFT : BaseEntity
    {
        public int Id { get; set; }

        public String Name { get; set; }
        public String Route { get; set; }
        public String Status { get; set; }
        public int Category { get; set; }
        public int Collection { get; set; }
        public decimal Price { get; set; }

        public override bool Equals(object subs)
        {
            if (subs == null)
            {
                return false;
            }
            if (!(subs is NFT))
            {
                return false;
            }
            return (this.Id == ((NFT)subs).Id);
        }
    }
}
