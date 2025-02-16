using AutoMapper;
using FUNewsManagement.BusinessObjects;
using FUNewsManagementMVC.Models;

namespace FUNewsManagementMVC.Mappings
{
    public class NewsArticleProfile : Profile
    {
        public NewsArticleProfile()
        {

            CreateMap<NewsArticle, NewsArticleVM>()
                .ForMember(d => d.TagIds, o => o.MapFrom(s => s.NewsTags.Select(n => n.TagID)))
                .ReverseMap();
        }
    }
}
