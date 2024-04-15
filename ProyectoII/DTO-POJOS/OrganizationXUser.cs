using System;

namespace DTO_POJOS
{
    public class OrganizationXUser : BaseEntity
    {
        public int Id { get; set; }
        public int Organzation { get; set; }
        public int User { get; set; }
    }
}
