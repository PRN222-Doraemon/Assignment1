namespace FUNewsManagement.BusinessObjects
{
    public class NewsTag
    {
        public string NewsArticleID { get; set; }
        public int TagID { get; set; }
        public virtual NewsArticle NewsArticle { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
