using System;


namespace DTO_POJOS
{
    public class Phone : BaseEntity
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public int User { get; set; }
    }
}
