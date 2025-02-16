using FUNewsManagement.BusinessObjects;

namespace FUNewsManagementMVC.Authentications
{
    public class AdminCredentials
    {
        public short AccountId { get; private set; } = 0;
        public string AccountName { get; private set; } = "ADMIN";
        public string AccountPassword { get; set; }
        public string AccountEmail { get; set; }
        public int AccountRole { get; private set; } = int.Parse(AppCts.Roles.Admin);
    }
}
