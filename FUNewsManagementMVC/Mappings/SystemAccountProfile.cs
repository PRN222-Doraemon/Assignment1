using AutoMapper;
using FUNewsManagement.BusinessObjects;
using FUNewsManagementMVC.Authentications;
using FUNewsManagementMVC.Models;

namespace FUNewsManagementMVC.Mappings
{
    public class SystemAccountProfile : Profile
    {
        public SystemAccountProfile()
        {
            CreateMap<SystemAccountVM, SystemAccount>().ReverseMap();

            CreateMap<AdminCredentials, SystemAccountVM>().ReverseMap();

            CreateMap<CreateSystemAccountVM, SystemAccount>().ReverseMap();
        }
    }
}
