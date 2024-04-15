using System;

namespace DTO_POJOS
{
    public class Acquisition : BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public decimal Price { get; set; }
        public int Buyer { get; set; }
        public int Creator { get; set; }
        public string FormattedCreationDate
        {
            get { return CreationDate.ToString("MM/dd/yyyy HH:mm"); }
        }
        public string FormattedClosingDate
        {
            get { return ClosingDate.ToString("MM/dd/yyyy HH:mm"); }
        }

    }
}
