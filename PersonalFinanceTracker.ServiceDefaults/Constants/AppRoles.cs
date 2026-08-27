namespace PersonalFinanceTracker.ServiceDefaults.Constants
{
    public class AppRoles
    {
        public const string USER = "User";
        public const string ADMIN = "Admin";

        public static bool IsValid(string role) {
            if (role is USER or ADMIN)
            {
                return true;
            }
            return false;
        }
    }
}
