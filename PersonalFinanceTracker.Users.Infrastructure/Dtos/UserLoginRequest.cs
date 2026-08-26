using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Users.Infrastructure.Dtos
{
    public record UserLoginRequest([Length(5, 50)] string UserName, [Length(5, 50)] string Password);
}
