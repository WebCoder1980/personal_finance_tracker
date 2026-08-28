using PersonalFinanceTracker.Transactions.Application.Categories.Handlers;
using PersonalFinanceTracker.Transactions.Application.Categories.Ports.In;
using PersonalFinanceTracker.Transactions.Application.Categories.Ports.Out;
using PersonalFinanceTracker.Transactions.Domain;

namespace PersonalFinanceTracker.Transactions.Infrastructure.Data
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
            IServiceScope scope = _scopeFactory.CreateScope();
            ICategoryRepository categoryRepository = scope.ServiceProvider.GetRequiredService<ICategoryRepository>();
            IUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            ICategoryCreateHandler categoryCreateHandler = scope.ServiceProvider.GetRequiredService<ICategoryCreateHandler>();

            if (!await categoryRepository.IsEmpty(token))
            {
                return;
            }

            IEnumerable<CategoryCreateCommand> commands = [
                new CategoryCreateCommand(Guid.Parse("9b11da16-cad0-4fe8-9a2f-c5c685a8ce49"), "Основная работа программистом", CategoryType.Income, 70000),
                new CategoryCreateCommand(Guid.Parse("9b11da16-cad0-4fe8-9a2f-c5c685a8ce49"), "Подработка на выходных", CategoryType.Income, 5000),
                new CategoryCreateCommand(Guid.Parse("9b11da16-cad0-4fe8-9a2f-c5c685a8ce49"), "Коммунальные платяжи", CategoryType.Expence, 10000),
                new CategoryCreateCommand(Guid.Parse("9b11da16-cad0-4fe8-9a2f-c5c685a8ce49"), "Еда", CategoryType.Expence, 15000),
                new CategoryCreateCommand(Guid.Parse("9b11da16-cad0-4fe8-9a2f-c5c685a8ce49"), "Онлайн подписки", CategoryType.Expence)
            ];

            foreach(CategoryCreateCommand command in commands) {
                await categoryCreateHandler.ExecuteAsync(command, token);
            }

            await unitOfWork.SaveChangesAsync(token);
        }

        public async Task StopAsync(CancellationToken token)
        {
            
        }
    }
}
