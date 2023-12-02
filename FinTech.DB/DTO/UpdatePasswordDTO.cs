﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.DB.DTO
{
    public  class UpdatePasswordDTO
    {
        [Required(ErrorMessage = "UserName is Required")]
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }
    }
}
