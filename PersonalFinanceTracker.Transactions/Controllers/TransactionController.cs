using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTracker.Domain.Dtos;
using PersonalFinanceTracker.Domain.Models;
using PersonalFinanceTracker.Transactions.Service;

namespace PersonalFinanceTracker.Transactions.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;
        public TransactionController(ITransactionService service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Transaction>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await _service.GetAsync(cancellationToken));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Transaction>> Create(TransactionUpsertRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _service.CreateAsync(request, cancellationToken));
        }

        [HttpPatch("{id:long}")]
        [Authorize]
        public async Task<ActionResult<Transaction>> UpdateById(long id, [FromBody] JsonPatchDocument<TransactionUpsertRequest> patchDoc, CancellationToken cancellationToken)
        {
            return Ok(await _service.UpdateAsync(id, patchDoc, cancellationToken));
        }

        [HttpDelete("{id:long}")]
        [Authorize]
        public async Task<ActionResult> DeleteById(long id, CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(id, cancellationToken);
            return Ok();
        }

    }
}
