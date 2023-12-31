﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.DB.DTO
{
    public  class UpdateAccountDTO
    {
        [Required]
        public string WalletFirstName { get; set; }
        [Required]
        public string WalletLastName { get; set; }
        [Required]
        [RegularExpression("[A-Za-z0-9!#%]{8,32}", ErrorMessage = "password must be between 8 and 32 letters")]
        public string WalletPassword { get; set; }
        [Required]
        [Compare("WalletPassword", ErrorMessage = "Wallet password must match")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
