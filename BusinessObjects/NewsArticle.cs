using System.ComponentModel.DataAnnotations;

namespace FUNewsManagement.BusinessObjects;

public partial class NewsArticle
{
    [Display(Name = "ID")]
    public string NewsArticleId { get; set; } = null!;
    [Display(Name = "News Title")]

    public string? NewsTitle { get; set; }

    public string Headline { get; set; } = null!;
    [Display(Name = "Created Date")]

    public DateTime? CreatedDate { get; set; }
    [Display(Name = "News Content")]

    public string? NewsContent { get; set; }
    [Display(Name = "News Source")]

    public string? NewsSource { get; set; }

    public short? CategoryId { get; set; }
    [Display(Name = "News Status")]

    public bool NewsStatus { get; set; } = true;

    public short? CreatedById { get; set; }

    public short? UpdatedById { get; set; }
    [Display(Name = "Modified Date")]

    public DateTime? ModifiedDate { get; set; }

    public virtual Category? Category { get; set; }
    [Display(Name = "Created By")]

    public virtual SystemAccount? CreatedBy { get; set; }

    public virtual ICollection<NewsTag> NewsTags { get; set; } = new List<NewsTag>();
}
