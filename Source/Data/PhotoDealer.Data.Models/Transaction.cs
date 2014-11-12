﻿using PhotoDealer.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoDealer.Data.Models
{
    public class CreditTransaction : AuditInfo, IDeletableEntity
    {
        public CreditTransaction()
        {
            this.TransactionId = Guid.NewGuid();
        }

        public Guid TransactionId { get; set; }

        public decimal Amount { get; set; }

        public string PictureId { get; set; }

        public Picture Picture { get; set; }


        public string SellerId { get; set; }

        public virtual User Seller { get; set; }

        public string BuyerId { get; set; }

        public virtual User Buyer { get; set; }


        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
