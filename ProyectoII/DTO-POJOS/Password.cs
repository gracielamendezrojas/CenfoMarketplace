using System;

namespace DTO_POJOS
{
    public class Password : BaseEntity
    {
        public int Id { get; set; }
        public string Passwordd { get; set; }
        public DateTime CreationDate { get; set; }
        public int User { get; set; }
    }
}
