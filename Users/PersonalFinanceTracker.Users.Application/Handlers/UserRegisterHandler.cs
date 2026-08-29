using PersonalFinanceTracker.Users.Application.Exceptions;
using PersonalFinanceTracker.Users.Application.Ports.In;
using PersonalFinanceTracker.Users.Application.Ports.Out;
using PersonalFinanceTracker.Users.Domain.Models;

namespace PersonalFinanceTracker.Users.Application.Handlers
{
    public class UserRegisterHandler : IUserRegisterHandler
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        public UserRegisterHandler(IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        public async Task<UserRegisterResult> ExecuteAsync(UserRegisterCommand command, CancellationToken token)
        {
            if (await _userRepository.UserNameIsBusyAsync(command.UserName, token))
            {
                throw new UserNameAlreadyExistsException();
            }

            User user = User.Register(
                command.Id,
                command.UserName,
                _passwordHasher.Hash(command.Password),
                command.Role
            );
            await _userRepository.SaveAsync(user, token);
            await _unitOfWork.SaveChangesAsync(token);

            return new UserRegisterResult(user.UserName, user.Role);
        }
    }
}
