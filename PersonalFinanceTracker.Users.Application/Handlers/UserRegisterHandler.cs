using PersonalFinanceTracker.Users.Application.Exceptions;
using PersonalFinanceTracker.Users.Application.Ports.In;
using PersonalFinanceTracker.Users.Application.Ports.Out;
using PersonalFinanceTracker.Users.Domain.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace PersonalFinanceTracker.Users.Application.Handlers
{
    public class UserRegisterHandler : IUserRegisterHandler
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        public UserRegisterHandler(IPasswordHasher passwordHasher, IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }
        public async Task<UserRegisterResult> ExecuteAsync(UserRegisterCommand command, CancellationToken token)
        {
            if (await _userRepository.UserNameIsBusyAsync(command.UserName, token))
            {
                throw new UserNameAlreadyExistsException();
            }

            User user = User.Register(
                command.UserName,
                _passwordHasher.Hash(command.Password)
            );
            await _userRepository.SaveAsync(user, token);
            await _userRepository.SaveChangesAsync();

            return new UserRegisterResult(user.UserName, user.Role);
        }
    }
}
