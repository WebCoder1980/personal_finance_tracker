namespace PersonalFinanceTracker.Users.Application.Handlers
{
    public record UserLoginResult(string Token, string UserName, string Role);
}
