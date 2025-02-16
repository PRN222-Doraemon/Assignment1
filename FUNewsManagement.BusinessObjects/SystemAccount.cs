using System.ComponentModel.DataAnnotations;

namespace FUNewsManagement.BusinessObjects;

public partial class SystemAccount
{
    [Key]
    public short AccountId { get; set; }

    [Display(Name = "Name")]
    public string? AccountName { get; set; }

    [Display(Name = "Email")]

    public string? AccountEmail { get; set; }
    [Display(Name = "Role")]

    public int? AccountRole { get; set; }

    public string? AccountPassword { get; set; }

    public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
}
