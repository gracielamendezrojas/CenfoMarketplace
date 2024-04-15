using System;

namespace DTO_POJOS
{
    public class TransactionXInvoice : BaseEntity
    {
        public int Id { get; set; }
        public int Suscription{ get; set; }
        public int Transaction { get; set; }
        public int Invoice_Id { get; set; }
    }
}
