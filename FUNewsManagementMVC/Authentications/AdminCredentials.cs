using FUNewsManagement.BusinessObjects;

namespace FUNewsManagementMVC.Authentications
{
    public class AdminCredentials
    {
        public int Id { get; private set; } = 0;
        public string Username { get; private set; } = "ADMIN";
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; private set; } = AppCts.Roles.Admin;
    }
}
