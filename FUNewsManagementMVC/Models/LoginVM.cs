using System.ComponentModel.DataAnnotations;

namespace FUNewsManagementMVC.Models
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "AccountEmail")]
        public string AccountEmail { get; set; }
        [Required]
        [Display(Name = "AccountPassword")]
        public string AccountPassword { get; set; }

    }
}
