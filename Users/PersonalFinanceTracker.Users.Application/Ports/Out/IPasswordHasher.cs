namespace PersonalFinanceTracker.Users.Application.Ports.Out
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string storedHash);
    }
}
