using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects;

public partial class Category
{
    [Display(Name = "Category ID")]
    public short CategoryId { get; set; }
    [Display(Name = "Category Name")]

    public string CategoryName { get; set; } = null!;
    [Display(Name = "Description")]

    public string CategoryDesciption { get; set; } = null!;

    public short? ParentCategoryId { get; set; }
    [Display(Name = "Active Status")]

    public bool? IsActive { get; set; }

    public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();

    public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
    [Display(Name = "Parent Category")]

    public virtual Category? ParentCategory { get; set; }
}
