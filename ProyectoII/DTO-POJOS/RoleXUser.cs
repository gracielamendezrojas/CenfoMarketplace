
using System;

namespace DTO_POJOS
{
    public class RoleXUser : BaseEntity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
