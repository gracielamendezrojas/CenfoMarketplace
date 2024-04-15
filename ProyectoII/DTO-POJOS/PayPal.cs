using System;

namespace DTO_POJOS
{
    public class Paypal : BaseEntity
    {
        public int Id { get; set; }
        public String OrderId { get; set; }
        public String OrderStatus { get; set; }
        public int UserId { get; set; }

    }
}
