using FUNewsManagement.BusinessObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using static FUNewsManagement.BusinessObjects.AppCts;

namespace FUNewsManagementMVC.Extensions
{
    public class RolesExtensions
    {
        public static IEnumerable<SelectListItem> GetRoleList()
        {
            return new List<(int, string)>()
            {
                (int.Parse(AppCts.Roles.Lecturer), nameof(AppCts.Roles.Lecturer)),
                (int.Parse(AppCts.Roles.Staff),  nameof(AppCts.Roles.Staff))
            }.Select(r => new SelectListItem()
            {
                Value = r.Item1.ToString(),
                Text = r.Item2,
            });
        }
    }
}

