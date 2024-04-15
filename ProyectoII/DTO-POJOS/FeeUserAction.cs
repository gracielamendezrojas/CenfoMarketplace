using System;

namespace DTO_POJOS
{
    public class FeeUserAction: BaseEntity
    {
        public int Admin { get; set; }
        public decimal Amount {get; set; }

    }
}
