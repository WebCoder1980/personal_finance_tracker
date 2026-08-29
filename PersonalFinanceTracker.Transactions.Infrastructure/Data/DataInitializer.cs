using PersonalFinanceTracker.Transactions.Application.Categories.Handlers;
using PersonalFinanceTracker.Transactions.Application.Categories.Ports.In;
using PersonalFinanceTracker.Transactions.Application.Categories.Ports.Out;
using PersonalFinanceTracker.Transactions.Application.Common.Ports.Out;
using PersonalFinanceTracker.Transactions.Application.Transactions.Handlers;
using PersonalFinanceTracker.Transactions.Application.Transactions.Ports.In;
using PersonalFinanceTracker.Transactions.Application.Transactions.Ports.Out;
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
            IUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            ICategoryRepository categoryRepository = scope.ServiceProvider.GetRequiredService<ICategoryRepository>();
            ICategoryCreateHandler categoryCreateHandler = scope.ServiceProvider.GetRequiredService<ICategoryCreateHandler>();
            ITransactionRepository transactionRepository = scope.ServiceProvider.GetRequiredService<ITransactionRepository>();
            ITransactionCreateHandler transactionCreateHandler = scope.ServiceProvider.GetRequiredService<ITransactionCreateHandler>();

            if (await categoryRepository.IsEmpty(token))
            {
                IEnumerable<CategoryCreateCommand> commands = [
                    new CategoryCreateCommand(Guid.Parse("e898c90e-12b1-47c5-89ac-ad09d8f956ad"), Guid.Parse("9b11da16-cad0-4fe8-9a2f-c5c685a8ce49"), "Основная работа программистом", CategoryType.Income, 70000),
                    new CategoryCreateCommand(Guid.Parse("bb5d1590-435c-4503-8bf4-c553076ba8e3"), Guid.Parse("9b11da16-cad0-4fe8-9a2f-c5c685a8ce49"), "Подработка на выходных", CategoryType.Income, 5000),
                    new CategoryCreateCommand(Guid.Parse("4628f644-65b3-4381-b419-579863dd8c03"), Guid.Parse("9b11da16-cad0-4fe8-9a2f-c5c685a8ce49"), "Коммунальные платяжи", CategoryType.Expence, 10000),
                    new CategoryCreateCommand(Guid.Parse("2b5a0bcd-2845-4ba9-94d5-225583829634"), Guid.Parse("9b11da16-cad0-4fe8-9a2f-c5c685a8ce49"), "Еда", CategoryType.Expence, 15000),
                    new CategoryCreateCommand(Guid.Parse("3ead3978-b60e-4d77-8ef1-4d4cb8612ae4"), Guid.Parse("9b11da16-cad0-4fe8-9a2f-c5c685a8ce49"), "Онлайн подписки", CategoryType.Expence, null)
                ];
                foreach (CategoryCreateCommand command in commands)
                {
                    await categoryCreateHandler.ExecuteAsync(command, token);
                }
            }

            if (await transactionRepository.IsEmpty(token))
            {
                IEnumerable<TransactionCreateCommand> commands = [
                    new TransactionCreateCommand(Guid.Parse("81a51b63-c610-4937-a010-0e1605dbbd31"), Guid.Parse("2b5a0bcd-2845-4ba9-94d5-225583829634"), new DateOnly(2026, 08, 29), 800, "Куринное филе 2кг"),
                    new TransactionCreateCommand(Guid.Parse("4b5a11b9-cb54-4eae-b04d-a403286cd5f3"), Guid.Parse("2b5a0bcd-2845-4ba9-94d5-225583829634"), new DateOnly(2026, 08, 28), 450, "Овощи и фрукты"),
                    new TransactionCreateCommand(Guid.Parse("e3ae436e-b274-43c0-94ca-9a237e96a747"), Guid.Parse("4628f644-65b3-4381-b419-579863dd8c03"), new DateOnly(2026, 08, 27), 3200, null),
                    new TransactionCreateCommand(Guid.Parse("5bab5f5d-0997-456c-a3fc-2db324f68ded"), Guid.Parse("3ead3978-b60e-4d77-8ef1-4d4cb8612ae4"), new DateOnly(2026, 08, 26), 599, null),
                    new TransactionCreateCommand(Guid.Parse("9f78558d-ab69-4755-a8bd-4efddb3ea2c2"), Guid.Parse("2b5a0bcd-2845-4ba9-94d5-225583829634"), new DateOnly(2026, 08, 25), 1200, "Разное"),
                    new TransactionCreateCommand(Guid.Parse("37bf5bf6-9810-4ac8-a601-612dae007821"), Guid.Parse("bb5d1590-435c-4503-8bf4-c553076ba8e3"), new DateOnly(2026, 08, 23), 2000, "3 часа"),
                    new TransactionCreateCommand(Guid.Parse("414e7e67-375b-4aee-b3f6-b22821c524b6"), Guid.Parse("e898c90e-12b1-47c5-89ac-ad09d8f956ad"), new DateOnly(2026, 08, 20), 70000, null)
                ];
                foreach (TransactionCreateCommand command in commands)
                {
                    await transactionCreateHandler.ExecuteAsync(command, token);
                }
            }

            await unitOfWork.SaveChangesAsync(token);
        }

        public async Task StopAsync(CancellationToken token)
        {
            
        }
    }
}
