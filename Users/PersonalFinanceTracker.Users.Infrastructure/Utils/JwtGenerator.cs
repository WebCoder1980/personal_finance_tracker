using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PersonalFinanceTracker.Users.Application.Ports.Out;
using PersonalFinanceTracker.Users.Domain.Models;
using PersonalFinanceTracker.Users.Infrastructure.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PersonalFinanceTracker.Users.Infrastructure.Utils
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly JwtOptions _jwtOptions;

        public JwtGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string Execute(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.UTF8.GetBytes(_jwtOptions.Key);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
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

            return jwt;
        }
    }
}
