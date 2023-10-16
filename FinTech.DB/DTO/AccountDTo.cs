using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.DB.DTO
{
    public  class AccountDTo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Account password must match")]
        public string ConfirmPassword { get; set; }
        public double Amount { get; set; } = 0;
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastUpdatedDate { get; set; }
    }
}
