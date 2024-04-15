using System;

namespace DTO_POJOS
{
    public class Document : BaseEntity
    {
        public int Id { get; set; }
        public String Filepath { get; set; }
        public String Description { get; set; }
        public int Subscription { get; set; }

    }
}
