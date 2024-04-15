using System;
using System.Globalization;

namespace DTO_POJOS
{
    public class UserAction : BaseEntity
    {
        //USERACTION
        public int Id { get; set; }
        public int User { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string FormattedDate
        {
            get { return Date.ToString("MM/dd/yyyy HH:mm"); }
        }
    }
}
