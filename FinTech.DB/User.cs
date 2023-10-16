using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.DB
{
    [Table("Users")]
    public  class User
    {
        public Guid userId { get; set; } = new Guid();
        public string firstName { get; set; }
        public string lastName { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime Created { get; set; }
        [Required]
        public string password { get; set; }
        ICollection<Account> accounts { get; set; }


        public string FullName()
        {
            return firstName + lastName;
        }

    }
}
