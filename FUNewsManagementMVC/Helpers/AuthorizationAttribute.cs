using FUNewsManagement.BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FUNewsManagementMVC.Helpers
{
    public class AuthorizationAttribute : ActionFilterAttribute
    {
        private readonly string _requiredRole;

        public AuthorizationAttribute(string requiredRole = null)
        {
            _requiredRole = requiredRole;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Authentication
            var userId = context.HttpContext.Session.GetInt32(AppCts.Session.UserId);
            if (userId == null)
            {
                context.Result = new RedirectToActionResult("Login", "SystemAccounts", null);
                return;
            }

            // Authorization
            if (_requiredRole != null)
            {
                var userRole = context.HttpContext.Session.GetInt32(AppCts.Session.UserRole);

                if (userRole == null || userRole.ToString() != _requiredRole)
                {
                    // if role is invalid, redirect to accessDenied page
                    context.Result = new RedirectToActionResult("AccessDenied", "SystemAccounts", null);
                }
            }
        }
    }
}
