using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.DB
{
    
    public  class Account
    {
        [Key]
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public double Amount { get; set; } = 0;
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string EmailAddress { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastUpdatedDate { get; set; }
        public User User { get; set; }
        Random random = new Random();
        public Account()
        {
            AccountNumber = Convert.ToString(Math.Floor(random.NextDouble() * 9_000_000_000L + 1_000_000_000L));
            AccountName = $"{LastName}{FirstName}";

        }
        public  string GenerateAccountNumber()
        {
            return Convert.ToString(Math.Floor(random.NextDouble() * 9_000_000_000L + 1_000_000_000L));
        }
        public string Fullname()
        {
            return $"{LastName}{FirstName}";
        }
    }
}
