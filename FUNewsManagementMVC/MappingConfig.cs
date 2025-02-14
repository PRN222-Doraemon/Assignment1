using AutoMapper;
using FUNewsManagement.BusinessObjects;
using FUNewsManagementMVC.Models;

namespace FUNewsManagementMVC
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<NewsArticleVM, NewsArticle>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
