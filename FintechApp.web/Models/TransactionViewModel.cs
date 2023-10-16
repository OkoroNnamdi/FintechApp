using FinTech.DB;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using static FinTech.DB.Transaction;

namespace FintechApp.web.Models
{
    public class TransactionViewModel
    {
        //[Display (Name = "Trans ID")]
        //public int TransactionId { get; set; }
        //[Display(Name = "Trans Reference")]
        //public string TransactionUniqueReference { get; set; }
        //[Display(Name = "Trans Status")]
        //public TranStatus TransactionStatus { get; set; }
        //[Display(Name = "Trans Type")]
        //public Transtype TransactionType { get; set; }
        //public bool IsSuccessful => TransactionStatus.Equals(TranStatus.Success);
        //[Display(Name = "Account")]
        //public string TransactionDestinationAccount { get; set; }
        //[Display(Name = "Particulars")]
        //public string TransactionParticulars { get; set; }
        //[Display(Name = "Amount")]
        //public decimal TransactionAmount { get; set; }
        //[Display(Name = "Date")]
        //public DateTime TransactionDate { get; set; }
        [Key]
        public int TransactionId { get; set; }
        public string TransactionUniqueReference { get; set; }
        public TranStatus TransactionStatus { get; set; }
        public Transtype TransactionType { get; set; }
        public bool IsSuccessful => TransactionStatus.Equals(TranStatus.Success);

        public string TransactionSourceAccount { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string TransactionDestinationAccount { get; set; }
        public string TransactionParticulars { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public Account account { get; set; }
    }
}
