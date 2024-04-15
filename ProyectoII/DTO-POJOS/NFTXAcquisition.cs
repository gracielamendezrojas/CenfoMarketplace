using System;

namespace DTO_POJOS
{
    public class NFTXAcquisition : BaseEntity
    {
        public int Id { get; set; }
        public int NFT { get; set; }
        public int Acquisition_Id { get; set; }
    }
}
