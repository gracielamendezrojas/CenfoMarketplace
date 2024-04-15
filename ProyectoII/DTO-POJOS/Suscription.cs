using System;

namespace DTO_POJOS
{
    public class Suscription : BaseEntity
    {
        public int Id { get; set; }
        public DateTime SuscriptionDate { get; set; }
        public int User { get; set; }
    }
}
//Suscription(SubscripcionManager.getAll().count + 1, name, something, something)