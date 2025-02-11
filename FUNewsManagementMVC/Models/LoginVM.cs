using System.ComponentModel.DataAnnotations;

namespace FUNewsManagementMVC.Models
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "Email")]
        public string AccountEmail { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string AccountPassword { get; set; }

    }
}
