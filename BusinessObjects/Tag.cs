using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects;

public partial class Tag
{
    [Display(Name = "Tag ID")]
    public int TagId { get; set; }
    [Display(Name = "Tag Name")]

    public string? TagName { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<NewsTag> NewsTags { get; set; } = new List<NewsTag>();
}
