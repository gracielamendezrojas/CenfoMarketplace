using System;

namespace DTO_POJOS
{
    public class CategoryXUser : BaseEntity
    {
        public int Id { get; set; }
        public int User { get; set; }
        public int Category { get; set; }
    }
}
