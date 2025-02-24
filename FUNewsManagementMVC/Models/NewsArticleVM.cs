﻿namespace FUNewsManagementMVC.Models
{
    public class NewsArticleVM
    {
        public string NewsArticleId { get; set; } = null!;

        public string? NewsTitle { get; set; }

        public string Headline { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }

        public string? NewsContent { get; set; }

        public string? NewsSource { get; set; }

        public short? CategoryId { get; set; }

        public bool NewsStatus { get; set; } = true;

        public short? CreatedById { get; set; }

        public short? UpdatedById { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<int> TagIds { get; set; } = new List<int>();
    }
}
