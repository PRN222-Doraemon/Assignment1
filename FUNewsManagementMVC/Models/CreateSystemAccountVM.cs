using System.ComponentModel.DataAnnotations;

namespace FUNewsManagementMVC.Models
{
    public class CreateSystemAccountVM
    {
        [Required]
        [Display(Name = "Name")]
        public required string AccountName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public required string AccountEmail { get; set; }


        [Required]
        [Display(Name = "Password")]
        public required string AccountPassword { get; set; }

        [Required]
        [Display(Name = "Role")]
        public required int AccountRole { get; set; }
    }
}
