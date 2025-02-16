using AutoMapper;
using FUNewsManagement.BusinessObjects;
using FUNewsManagementMVC.Models;

namespace FUNewsManagementMVC.Mappings
{
    public class SystemAccountProfile : Profile
    {
        public SystemAccountProfile()
        {
            CreateMap<SystemAccountVM, SystemAccount>().ReverseMap();
        }
    }
}
