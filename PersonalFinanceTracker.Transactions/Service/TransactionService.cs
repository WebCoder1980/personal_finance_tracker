using Microsoft.AspNetCore.JsonPatch;
using PersonalFinanceTracker.Domain.Dtos;
using PersonalFinanceTracker.Domain.Models;
using PersonalFinanceTracker.Domain.Converters;
using PersonalFinanceTracker.Transactions.Service.Auth;
using PersonalFinanceTracker.Transactions.Data;

namespace PersonalFinanceTracker.Transactions.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly TransactionRepository _repository;
        private readonly ICurrentUser _currentUser;

        public TransactionService(TransactionRepository repository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }
        public async Task<IEnumerable<Transaction>> GetAsync(CancellationToken cancellationToken)
        {
            long userId = _currentUser.Id;
            return await _repository.GetByUserIdAsync(userId, cancellationToken);
        }
        public async Task<Transaction> CreateAsync(TransactionUpsertRequest request, CancellationToken cancellationToken)
        {
            Transaction model = request.ToModel();
            model.UserId = _currentUser.Id;
            return await _repository.CreateAsync(model, cancellationToken);
        }
        public async Task<Transaction> UpdateAsync(long id, JsonPatchDocument<TransactionUpsertRequest> patchDoc, CancellationToken cancellationToken)
        {
            Transaction model = await _repository.GetByIdAndUserIdAsync(id, _currentUser.Id);
            TransactionUpsertRequest dto = model.ToDto();
            patchDoc.ApplyTo(dto);
            model.UpdateFrom(dto);
            
            return await _repository.UpdateAsync(model, cancellationToken);
        }
        public async Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            Transaction model = await _repository.GetByIdAndUserIdAsync(id, _currentUser.Id);

            await _repository.DeleteAsync(model, cancellationToken);
        }
    }
}
