using AutoMapper;
using FUNewsManagement.BusinessObjects;
using FUNewsManagementMVC.Models;

namespace FUNewsManagementMVC.Mappings
{
    public class NewsArticleProfile : Profile
    {
        public NewsArticleProfile() {

            CreateMap<NewsArticle, NewsArticleVM>().ReverseMap();
        }
    }
}
