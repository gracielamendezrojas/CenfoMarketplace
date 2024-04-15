
using System;

namespace DTO_POJOS
{
    public class Wallet: BaseEntity
    {
        public string Id { get; set; }
        public decimal Balance { get; set; }
        public int UserId { get; set; }

    }
}
