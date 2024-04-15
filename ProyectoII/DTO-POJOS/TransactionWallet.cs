using System;

namespace DTO_POJOS
{
    public class TransactionWallet : BaseEntity
    {
        public int Id { get; set; }
        public string WalletSender { get; set; }
        public string WalletReceiver { get; set; }
        public decimal Amount { get; set; }
        public String Description { get; set; }
        public  DateTime TransactionDate { get; set; }
        public string FormattedDate
        {
            get { return TransactionDate.ToString("MM/dd/yyyy HH:mm"); }
        }

    }
}
