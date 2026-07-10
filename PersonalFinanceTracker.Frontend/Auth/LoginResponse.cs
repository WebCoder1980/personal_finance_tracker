namespace PersonalFinanceTracker.Frontend.Auth;

public sealed class LoginResponse
{
    public string Token { get; set; } = "";
    public string UserName { get; set; } = "";
    public string Role { get; set; } = "";
}
