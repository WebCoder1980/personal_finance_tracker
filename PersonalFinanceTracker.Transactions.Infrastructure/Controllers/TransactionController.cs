using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTracker.Transactions.Application.Transactions.Handlers;
using PersonalFinanceTracker.Transactions.Application.Transactions.Ports.In;
using PersonalFinanceTracker.Transactions.Infrastructure.Dtos;
using PersonalFinanceTracker.Transactions.Infrastructure.Util;

namespace PersonalFinanceTracker.Transactions.Infrastructure.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ICurrentUser _currentUser;
        private readonly ITransactionGetHandler _TransactionGetHandler;
        private readonly ITransactionCreateHandler _TransactionCreateHandler;
        private readonly ITransactionUpdateHandler _TransactionUpdateHandler;
        private readonly ITransactionDeleteHandler _TransactionDeleteHandler;

        public TransactionController(ICurrentUser currentUser, ITransactionGetHandler TransactionGetHandler, ITransactionCreateHandler TransactionCreateHandler, ITransactionUpdateHandler TransactionUpdateHandler, ITransactionDeleteHandler TransactionDeleteHandler)
        {
            _currentUser = currentUser;
            _TransactionGetHandler = TransactionGetHandler;
            _TransactionCreateHandler = TransactionCreateHandler;
            _TransactionUpdateHandler = TransactionUpdateHandler;
            _TransactionDeleteHandler = TransactionDeleteHandler;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TransactionGetResult>>> Get(CancellationToken token)
        {
            TransactionGetCommand command = new(_currentUser.Id);

            return Ok(await _TransactionGetHandler.ExecuteAsync(command, token));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<TransactionCreateResult>> Create(TransactionCreateRequest request, CancellationToken token)
        {
            TransactionCreateCommand command = new(_currentUser.Id, request.Date, request.Value, request.Comment);

            return Ok(await _TransactionCreateHandler.ExecuteAsync(command, token));
        }

        [HttpPut("{id:Guid}")]
        [Authorize]
        public async Task<ActionResult<TransactionUpdateResult>> UpdateById(Guid id, [FromBody] TransactionUpdateRequest request, CancellationToken token)
        {
            TransactionUpdateCommand command = new(id, _currentUser.Id, request.Date, request.Value, request.Comment);

            return Ok(await _TransactionUpdateHandler.ExecuteAsync(command, token));
        }

        [HttpDelete("{id:Guid}")]
        [Authorize]
        public async Task<ActionResult> DeleteById(Guid id, CancellationToken token)
        {
            TransactionDeleteCommand command = new(id, _currentUser.Id);

            await _TransactionDeleteHandler.ExecuteAsync(command, token);

            return Ok();
        }
    }
}
