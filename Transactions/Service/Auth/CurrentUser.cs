namespace Transactions.Service.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long? Id {
            get
            {
                string? idStr = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(claim => claim.Type == "user_id")?.Value;
                if (idStr is null) {
                    return null;
                }
                long id;
                if (!long.TryParse(idStr, out id))
                {
                    throw new UnauthorizedAccessException("jwt содержит некорректный 'user_id'");
                }
                return id;
            }
        }
    }
}
