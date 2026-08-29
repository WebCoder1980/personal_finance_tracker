using PersonalFinanceTracker.ServiceDefaults.Exceptions;
using PersonalFinanceTracker.Users.Application.Ports.In;
using PersonalFinanceTracker.Users.Application.Ports.Out;
using PersonalFinanceTracker.Users.Domain.Models;

namespace PersonalFinanceTracker.Users.Application.Handlers
{
    public class UserLoginHandler : IUserLoginHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtGenerator _jwtGenerator;

        public UserLoginHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtGenerator jwtGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<UserLoginResult> ExecuteAsync(UserLoginCommand command, CancellationToken token)
        {
            User user = await _userRepository.GetByUserName(command.UserName, token)
                ?? throw new DomainException("User was not found");
            if (!_passwordHasher.Verify(command.Password, user.PasswordHash))
            {
                throw new DomainException("Invalid credentials");
            }

            string jwt = _jwtGenerator.Execute(user);
            return new UserLoginResult(jwt, user.UserName, user.Role);
        }
    }
}
