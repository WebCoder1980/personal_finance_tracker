using PersonalFinanceTracker.ServiceDefaults.Exceptions;
using PersonalFinanceTracker.Transactions.Application.Common.Ports.Out;
using PersonalFinanceTracker.Transactions.Application.Transactions.Ports.In;
using PersonalFinanceTracker.Transactions.Application.Transactions.Ports.Out;
using PersonalFinanceTracker.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Transactions.Handlers
{
    public class TransactionCreateHandler : ITransactionCreateHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionCreateHandler(IUnitOfWork unitOfWork, ITransactionRepository transactionRepository)
        {
            _unitOfWork = unitOfWork;
            _transactionRepository = transactionRepository;
        }

        public async Task<TransactionCreateResult> ExecuteAsync(TransactionCreateCommand command, CancellationToken token)
        {
            Transaction transaction = Transaction.Create(command.Id, command.CategoryId, command.Date, command.Value, command.Comment);

            await _transactionRepository.SaveAsync(transaction, token);
            await _unitOfWork.SaveChangesAsync(token);

            return new TransactionCreateResult(transaction.Id, transaction.CategoryId, transaction.Date, transaction.Value, transaction.Comment);
        }
    }
}
