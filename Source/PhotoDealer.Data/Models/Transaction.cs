using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoDealer.Data.Models
{
    public class Transaction
    {
        public Transaction()
        {
            this.TransactionId = Guid.NewGuid();
        }

        public Guid TransactionId { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }


        public string SellerId { get; set; }

        public virtual User Seller { get; set; }

        public string BuyerId { get; set; }

        public virtual User Buyer { get; set; }
    }
}
