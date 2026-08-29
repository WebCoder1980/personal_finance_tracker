using PersonalFinanceTracker.ServiceDefaults.Exceptions;
using PersonalFinanceTracker.Transactions.Application.Common.Ports.Out;
using PersonalFinanceTracker.Transactions.Application.Transactions.Ports.In;
using PersonalFinanceTracker.Transactions.Application.Transactions.Ports.Out;
using PersonalFinanceTracker.Transactions.Domain;

namespace PersonalFinanceTracker.Transactions.Application.Transactions.Handlers
{
    public class TransactionUpdateHandler : ITransactionUpdateHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionUpdateHandler(IUnitOfWork unitOfWork, ITransactionRepository transactionRepository)
        {
            _unitOfWork = unitOfWork;
            _transactionRepository = transactionRepository;
        }

        public async Task<TransactionUpdateResult> ExecuteAsync(TransactionUpdateCommand command, CancellationToken token)
        {
            Transaction transaction = await _transactionRepository.GetByIdAsync(command.Id, token)
                ?? throw new NotFoundException("Transaction was not found");

            if (!transaction.HasAccess(command.UserId))
            {
                throw new PermissionDeniedException();
            }

            transaction.ChangeDate(command.Date);
            transaction.ChangeValue(command.Value);
            transaction.ChangeComment(command.Comment);

            await _unitOfWork.SaveChangesAsync(token);

            return new TransactionUpdateResult(transaction.Id, transaction.CategoryId, transaction.Date, transaction.Value, transaction.Comment);
        }
    }
}
