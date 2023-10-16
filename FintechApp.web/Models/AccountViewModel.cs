using System.ComponentModel.DataAnnotations;

namespace FintechApp.web.Models
{
    public class AccountViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Account password must match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Balance")]
        public double Amount { get; set; } = 0;
        public string AccountNumber { get; set; }
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastUpdatedDate { get; set; }
    }
}
