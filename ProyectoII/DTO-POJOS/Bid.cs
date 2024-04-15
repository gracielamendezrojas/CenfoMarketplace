using System;

namespace DTO_POJOS
{
    public class Bid : BaseEntity
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int Acquisition { get; set; }
        public int User { get; set; }
        public int Type { get; set; } // 1 si es NFT 2 si es collection
        public string FormattedDate
        {
            get { return Date.ToString("MM/dd/yyyy HH:mm"); }
        }
    }
}
