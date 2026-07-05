using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Users.Constants;
using Users.Data;
using Users.Dtos;
using Users.Exceptions;
using Users.MessageBuses;
using Users.Models;

namespace Users.Service
{
    public class AuthService : IAuthService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly AppDbContext _dbContext;
        private readonly IMessageBus _messageBus;

        public AuthService(IOptions<JwtOptions> jwtOptions, AppDbContext dbContext, IMessageBus messageBus)
        {
            _jwtOptions = jwtOptions.Value;
            _dbContext = dbContext;
            _messageBus = messageBus;
        }
        public async Task<LoginResponse?> Login(Dtos.LoginRequest request)
        {
            User? user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.UserName == request.UserName);
            if (user == null)
            {
                throw new InvalidCredentialsException();
            }

            if (!AuthUtil.VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new InvalidCredentialsException();
            }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.UTF8.GetBytes(_jwtOptions.Key);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("user_id", user.Id.ToString())
            };

            SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            SecurityToken token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresMinutes),
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                SigningCredentials = credentials
            });

            string jwt = tokenHandler.WriteToken(token);

            return new LoginResponse(jwt, request.UserName, user.Role);
        }

        public async Task<RegisterResponse?> Register(RegisterRequest request)
        {
            bool userExists = await _dbContext.Users.AnyAsync(user => user.UserName == request.UserName);
            if (userExists)
            {
                throw new UserAlreadyExistsException();
            }

            User user = new User
            {
                UserName = request.UserName,
                PasswordHash = AuthUtil.HashPassword(request.Password),
                Role = AppRoles.USER
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            await _messageBus.SendUserCreatedAsync(user);

            return new RegisterResponse(user.UserName, user.Role);
        }
    }
}
