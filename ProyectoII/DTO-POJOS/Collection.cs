using System;

namespace DTO_POJOS
{
    public class Collection : BaseEntity
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Status { get; set; }
        public String Description { get; set; }
        public String SaleStatus { get; set; }
        public int User { get; set; }
    }
}
