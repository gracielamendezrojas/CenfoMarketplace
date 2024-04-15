using System;


namespace DTO_POJOS
{
    public class Notification : BaseEntity
    {
        public int Id { get; set; }
        public int User { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
    }
}
