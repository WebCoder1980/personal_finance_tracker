using PersonalFinanceTracker.ServiceDefaults.Exceptions;
using PersonalFinanceTracker.Transactions.Application.Common.Ports.Out;
using PersonalFinanceTracker.Transactions.Application.Transactions.Ports.In;
using PersonalFinanceTracker.Transactions.Application.Transactions.Ports.Out;
using PersonalFinanceTracker.Transactions.Domain;

namespace PersonalFinanceTracker.Transactions.Application.Transactions.Handlers
{
    public class TransactionDeleteHandler : ITransactionDeleteHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionDeleteHandler(IUnitOfWork unitOfWork, ITransactionRepository transactionRepository)
        {
            _unitOfWork = unitOfWork;
            _transactionRepository = transactionRepository;
        }

        public async Task ExecuteAsync(TransactionDeleteCommand command, CancellationToken token)
        {
            Transaction transaction = await _transactionRepository.GetByIdAsync(command.Id, token)
                ?? throw new NotFoundException("Transaction was not found");

            if (!transaction.HasAccess(command.UserId))
            {
                throw new PermissionDeniedException();
            }

            _transactionRepository.DeleteAsync(transaction);
            await _unitOfWork.SaveChangesAsync(token);
        }
    }
}
