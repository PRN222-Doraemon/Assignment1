using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace FUNewsManagementMVC.Models
{
    public class SystemAccountVM
    {
        [ValidateNever]
        public short AccountId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string? AccountName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string? AccountEmail { get; set; }

        [Required]
        [Display(Name = "Role")]
        public int? AccountRole { get; set; }

        [Display(Name = "Password")]
        public string? AccountPassword { get; set; }
    }
}
