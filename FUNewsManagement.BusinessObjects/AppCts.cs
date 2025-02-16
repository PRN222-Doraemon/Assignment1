namespace FUNewsManagement.BusinessObjects;

public static class AppCts
{
    public static class Session
    {
        public const string UserId = "UserId";

        public const string UserEmail = "UserEmail";

        public const string UserRole = "UserRole";

        public const string UserName = "UserName";
    }

    public static class Roles
    {
        public const string Admin = "0";
        public const string Staff = "1";
        public const string Lecturer = "2";
    }

    public static class Status
    {
        public const string Active = "1";
        public const string Inactive = "1";
    }
}
