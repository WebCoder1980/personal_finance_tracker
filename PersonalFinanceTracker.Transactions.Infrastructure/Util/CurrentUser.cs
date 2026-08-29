namespace PersonalFinanceTracker.Transactions.Infrastructure.Util
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid Id
        {
            get
            {
                string idStr = _httpContextAccessor.HttpContext?.User.Claims
                    .FirstOrDefault(claim => claim.Type == "user_id")?.Value
                    ?? throw new UnauthorizedAccessException("JWT does not contain 'user_id'.");
                Guid id;
                if (!Guid.TryParse(idStr, out id))
                {
                    throw new UnauthorizedAccessException("jwt содержит некорректный 'user_id'");
                }
                return id;
            }
        }
    }
}
