using System;

namespace DTO_POJOS
{
    public class Invoice : BaseEntity
    {
        //public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        // public String Detail { get; set; }
        public string  NFT { get; set; }
        public string Collection { get; set; }
        public int Price { get; set; }
        //public string URLImage { get; set; }
        //public int UserId { get; set; }
    }
}
