using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.DB.DTO
{
    public  class TransactionDTO
    {
        public Transaction.Transtype TransactionType { get; set; }

        public string TransactionSourceAccount { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
