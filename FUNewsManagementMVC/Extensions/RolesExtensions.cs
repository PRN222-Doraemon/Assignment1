using FUNewsManagement.BusinessObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using static FUNewsManagement.BusinessObjects.AppCts;

namespace FUNewsManagementMVC.Extensions
{
    public class RolesExtensions
    {
        public static List<SelectListItem> GetRoleList()
        {
            return Enum.GetValues(typeof(AccountRoles))
                .Cast<AccountRoles>()
                .Select(r => new SelectListItem
                {
                    Value = ((int)r).ToString(),
                    Text = r.ToString()
                })
                .ToList();
        }
    }
}
