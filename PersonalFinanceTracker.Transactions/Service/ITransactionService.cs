using Microsoft.AspNetCore.JsonPatch;
using PersonalFinanceTracker.Domain.Dtos;
using PersonalFinanceTracker.Domain.Models;

namespace PersonalFinanceTracker.Transactions.Service
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAsync(CancellationToken cancellationToken);
        Task<Transaction> CreateAsync(TransactionUpsertRequest request, CancellationToken cancellationToken);
        Task<Transaction> UpdateAsync(long transactionId, JsonPatchDocument<TransactionUpsertRequest> patchDoc, CancellationToken cancellationToken);
        Task DeleteAsync(long id, CancellationToken cancellationToken);
    }
}
