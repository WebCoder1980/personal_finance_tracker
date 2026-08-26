using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.OpenApi;
using PersonalFinanceTracker.Users.Application.Handlers;
using PersonalFinanceTracker.Users.Application.Ports.In;
using PersonalFinanceTracker.Users.Application.Ports.Out;
using PersonalFinanceTracker.Users.Domain.Constants;
using PersonalFinanceTracker.Users.Domain.Models;
using System.Runtime.CompilerServices;

namespace PersonalFinanceTracker.Users.Infrastructure.Data
{
    public class DataInitializer : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DataInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken token)
        {
            List<UserRegisterCommand> commands = [
                new UserRegisterCommand("admin", "admin_password", AppRoles.ADMIN),
                new UserRegisterCommand("maxsmg", "qweqwe", AppRoles.USER)
            ];

            using IServiceScope scope = _scopeFactory.CreateScope();

            IUserRegisterHandler userRegisterHandler = scope.ServiceProvider.GetRequiredService<IUserRegisterHandler>();
            IUserRepository userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

            foreach (UserRegisterCommand command in commands)
            {
                if (await userRepository.UserNameIsBusyAsync(command.UserName, token))
                {
                    continue;
                }

                await userRegisterHandler.ExecuteAsync(command, token);
            }
        }

        public async Task StopAsync(CancellationToken token)
        {
            
        }
    }
}
